using System;
using System.IO;

using Ionic.Zlib;
using FirstFloor.Documents.IO;

namespace FirstFloor.Documents.Pdf.Filters
{
    /// <summary>
    /// Decompresses data encoded using the zlib/deflate compression method, 
    /// </summary>
    internal class FlateDecode
        : Filter
    {
        private Stream output;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlateDecode"/> class.
        /// </summary>
        /// <param name="baseStream"></param>
        /// <param name="parameters">The parameters.</param>
        public FlateDecode(Stream baseStream, DictionaryObject parameters)
            : base(baseStream, parameters)
        {
            // NOTE: taking the easy road, decode immediately and store result in memory stream
            // we can do better here, but ZlibStream isn't seekable
            Decode();
        }

        private void Decode()
        {
            int predictor = 1;
            if (this.Parameters != null) {
                predictor = this.Parameters.Get<int>("Predictor", 1);
            }

            using (var stream = new ZlibStream(this.BaseStream, CompressionMode.Decompress)) {
                this.output = DecodeFilter(stream, predictor);
            }
        }

        private Stream DecodeFilter(Stream stream, int predictor)
        {
            if (predictor == 1) {
                // no prediction algorithm, decode only

                // BUG: buffer size matters, if larger decoding fails, this is a bug in ZlibStream?
                return stream.ToMemoryStream(1024);
            }
            if (predictor == 2) {
                throw new PdfParseException(Resources.UnsupportedTiff2Predictor);
            }
            var colors = this.Parameters.Get<int>("Colors", 1);
            var bitsPerComponent = this.Parameters.Get<int>("BitsPerComponent", 8);
            var columns = this.Parameters.Get<int>("Columns", 1);

            var bpp = (colors * bitsPerComponent + 7) / 8;
            var rowLength = (columns * colors * bitsPerComponent + 7) / 8;
            var row = new byte[rowLength];
            var priorRow = new byte[rowLength];
            var output = new MemoryStream();
            while (true) {
                predictor = stream.ReadByte();
                if (predictor == -1) {
                    break;  // end of stream
                }
                if (stream.Read(row, 0, rowLength) != rowLength) {
                    throw new PdfParseException(Resources.UnexpectedEndOfStream);
                }

                if (predictor == 0) {
                    // PNG None
                    // nothing here, in == out
                }
                else if (predictor == 1) {
                    // PNG Sub
                    for (var x = 0; x < rowLength; x++) {
                        var left = (x - bpp >= 0) ? row[x-bpp] : 0;
                        row[x] = (byte)((row[x] + left) % 0x100);
                    }
                }
                else if (predictor == 2) {
                    // PNG Up
                    for (int x = 0; x < rowLength; x++) {
                        var prior = priorRow[x];
                        row[x] = (byte)((row[x] + prior) % 0x100);
                    }
                }
                else if (predictor == 3) {
                    // PNG Average
                    for (int x = 0; x < rowLength; x++) {
                        var left = (x - bpp >= 0) ? row[x - bpp] : 0;
                        var prior = priorRow[x];
                        row[x] = (byte)((row[x] + Math.Floor(left + prior) / 2) % 0x100);
                    }
                }
                else if (predictor == 4) {
                    // PNG Paeth
                    for (int x = 0; x < rowLength; x++) {
                        byte left = 0;
                        byte priorLeft = 0;
                        if (x - bpp >= 0) {
                            left = row[x - bpp];
                            priorLeft = priorRow[x - bpp];
                        }
                        byte prior = priorRow[x];
                        
                        row[x] = (byte)((row[x] + PaethPredictor(left, prior, priorLeft)) % 0x100);
                    }
                }
                else {
                    throw new PdfParseException(Resources.UnknownPredictor, predictor);
                }

                // copy prior row
                for (var i = 0; i < rowLength; i++) {
                    priorRow[i] = row[i];
                }
                output.Write(row, 0, row.Length);
            }

            output.Seek(0, SeekOrigin.Begin);

            return output;
        }

        /// <summary>
        /// The Paeth predictor function.
        /// </summary>
        /// <param name="a">Left.</param>
        /// <param name="b">Above.</param>
        /// <param name="c">Upper left.</param>
        /// <returns></returns>
        private static int PaethPredictor(byte a, byte b, byte c)
        {
            var p = a + b - c;              // initial estimate
            var pa = Math.Abs(p - a);       // distances to a, b, c
            var pb = Math.Abs(p - b);
            var pc = Math.Abs(p - c);
            // return nearest of a,b,c
            // breaking ties in order a,b,c
            if (pa <= pb && pa <= pc) {
                return a;
            }
            if (pb <= pc) {
                return b;
            }
            return c;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.IO.Stream"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing) {
                if (this.output != null) {
                    this.output.Dispose();
                    this.output = null;
                }
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the length in bytes of the stream.
        /// </summary>
        /// <value></value>
        /// <returns>A long value representing the length of the stream in bytes.</returns>
        /// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Length
        {
            get { return this.output.Length; }
        }

        /// <summary>
        /// When overridden in a derived class, gets or sets the position within the current stream.
        /// </summary>
        /// <value></value>
        /// <returns>The current position within the stream.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Position
        {
            get { return this.output.Position; }
            set { this.output.Position = value; }
        }

        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>
        /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length. </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="buffer"/> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="offset"/> or <paramref name="count"/> is negative. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.output.Read(buffer, offset, count);
        }

        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <returns>
        /// The new position within the current stream.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.output.Seek(offset, origin);
        }
    }
}
