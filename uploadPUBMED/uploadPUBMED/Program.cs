using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace uploadPUBMED
{
    class Program
    {
        static void Main(string[] args)
        {

            string dir = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_REFERENCES_DATABASES\_RETINA_split";

            string[] files = Directory.GetFiles(dir, "*.xml");

            int count = 0;

            foreach (string filename in files)
            {
                
                count = count + 1;

                Console.WriteLine(Convert.ToString(count));

                myData mydata = new myData();

                string xml = File.ReadAllText(filename);

                // PMID

                int idx_pmid = xml.IndexOf("<PMID Version=\"1\">");

                if (idx_pmid != -1)
                {
                    int idx_pmid_end = xml.IndexOf("</PMID>", idx_pmid);

                    string pmid = xml.Substring(idx_pmid + "<PMID Version=\"1\">".Length, idx_pmid_end - idx_pmid - "<PMID Version=\"1\">".Length);

                    mydata.PMIDv1 = pmid;
                }

                idx_pmid = xml.IndexOf("<PMID Version=\"2\">");

                if (idx_pmid != -1)
                {
                    int idx_pmid_end = xml.IndexOf("</PMID>", idx_pmid);

                    string pmid = xml.Substring(idx_pmid + "<PMID Version=\"2\">".Length, idx_pmid_end - idx_pmid - "<PMID Version=\"2\">".Length);

                    mydata.PMIDv2 = pmid;
                }

                idx_pmid = xml.IndexOf("<PMID Version=\"3\">");

                if (idx_pmid != -1)
                {
                    int idx_pmid_end = xml.IndexOf("</PMID>", idx_pmid);

                    string pmid = xml.Substring(idx_pmid + "<PMID Version=\"3\">".Length, idx_pmid_end - idx_pmid - "<PMID Version=\"3\">".Length);

                    mydata.PMIDv3 = pmid;
                }


                // ABSTRACT

                int idx_abstract = xml.IndexOf("<Abstract>");

                if (idx_abstract != -1)
                {
                    int idx_abstract_end = xml.IndexOf("</Abstract>", idx_abstract);

                    string abs = xml.Substring(idx_abstract + "<Abstract>".Length, idx_abstract_end - idx_abstract - "<Abstract>".Length);

                    abs = abs.Replace("<AbstractText>", " ");
                    abs = abs.Replace("<AbstractText Label = \"INTRODUCTION\" NlmCategory = \"BACKGROUND\">", " ");
                    abs = abs.Replace("<AbstractText Label = \"AIMS\" NlmCategory = \"OBJECTIVE\">", " ");
                    abs = abs.Replace("<AbstractText Label = \"DESIGN, SETTING AND PARTICIPANTS\" NlmCategory = \"METHODS\">", " ");
                    abs = abs.Replace("<AbstractText Label = \"RESULTS\" NlmCategory = \"RESULTS\">", " ");
                    abs = abs.Replace("<AbstractText Label = \"DISCUSSION\" NlmCategory = \"CONCLUSIONS\">", " ");
                    abs = abs.Replace("</AbstractText>", " ");

                    abs = abs.Trim(' ');
                    abs = abs.Trim(' ');
                    abs = abs.Trim(' ');
                    abs = abs.Trim(' ');
                    abs = abs.Trim(' ');
                    abs = abs.Trim(' ');
                    abs = abs.Trim(' ');
                    abs = abs.Trim(' ');
                    abs = abs.Trim(' ');
                    abs = abs.Trim(' ');

                    mydata.Abstract = abs;
                }
                    
                // PUB DATE

                int idx_pub_date = xml.IndexOf("<PubDate>");

                if (idx_pub_date != -1)
                {
                    int idx_pub_date_end = xml.IndexOf("</PubDate>", idx_pub_date);

                    string date = xml.Substring(idx_pub_date + "<PubDate>".Length, idx_pub_date_end - idx_pub_date - "<PubDate>".Length);

                    int idx_day = date.IndexOf("<Day>");

                    if (idx_day != -1)
                    {
                        int idx_day_end = date.IndexOf("</Day>", idx_day);

                        string day = date.Substring(idx_day + "<Day>".Length, idx_day_end - idx_day - "<Day>".Length);

                        mydata.PubDate_Day = day;
                    }

                    int idx_month = date.IndexOf("<Month>");

                    if (idx_month != -1)
                    {
                        int idx_month_end = date.IndexOf("</Month>", idx_month);

                        string month = date.Substring(idx_month + "<Month>".Length, idx_month_end - idx_month - "<Month>".Length);

                        mydata.PubDate_Month = month;
                    }

                    int idx_year = date.IndexOf("<Year>");

                    if (idx_year != -1)
                    {
                        int idx_year_end = date.IndexOf("</Year>", idx_year);

                        string year = date.Substring(idx_year + "<Year>".Length, idx_year_end - idx_year - "<Year>".Length);

                        mydata.PubDate_Year = year;
                    }

                }

                // ARTICLE TITLE

                int idx_article_title = xml.IndexOf("<ArticleTitle>");

                if (idx_article_title != -1)
                {
                    int idx_article_title_end = xml.IndexOf("</ArticleTitle>", idx_article_title);

                    string article_title = xml.Substring(idx_article_title + "<ArticleTitle>".Length, idx_article_title_end - idx_article_title - "<ArticleTitle>".Length);

                    mydata.ArticleTitle = article_title;
                        
                }

                // OTHER ABSTRACT

                int idx_other_abstract = xml.IndexOf("<OtherAbstract>");

                if (idx_other_abstract != -1)
                {
                    int idx_other_abstract_end = xml.IndexOf("</OtherAbstract>", idx_other_abstract);

                    string otherabs = xml.Substring(idx_other_abstract + "<OtherAbstract>".Length, idx_other_abstract_end - idx_other_abstract - "<OtherAbstract>".Length);

                    mydata.OtherAbstract = otherabs;
                }

                // JOURNAL

                int idx_journal = xml.IndexOf("<Journal>");

                if (idx_journal != -1)
                {
                    int idx_journal_end = xml.IndexOf("</Journal>", idx_journal);

                    string journal = xml.Substring(idx_journal + "<Journal>".Length, idx_journal_end - idx_journal - "<Journal>".Length);

                    //TITLE

                    int idx_journal_title = journal.IndexOf("<Title>");

                    if (idx_journal_title != -1)
                    {
                        int idx_journal_title_end = journal.IndexOf("</Title>", idx_journal_title);

                        string journal_title = journal.Substring(idx_journal_title + "<Title>".Length, idx_journal_title_end - idx_journal_title - "<Title>".Length);

                        mydata.JournalTitle = journal_title;
                    }

                    //ISSUE

                    int idx_journal_issue = journal.IndexOf("<Issue>");

                    if (idx_journal_issue != -1)
                    {
                        int idx_journal_issue_end = journal.IndexOf("</Issue>", idx_journal_issue);

                        string journal_issue = journal.Substring(idx_journal_issue + "<Issue>".Length, idx_journal_issue_end - idx_journal_issue - "<Issue>".Length);

                        mydata.JournalIssue = journal_issue;
                    }

                    //VOLUME

                    int idx_journal_volume = journal.IndexOf("<Volume>");

                    if (idx_journal_volume != -1)
                    {
                        int idx_journal_volume_end = journal.IndexOf("</Volume>", idx_journal_volume);

                        string journal_volume = journal.Substring(idx_journal_volume + "<Volume>".Length, idx_journal_volume_end - idx_journal_volume - "<Volume>".Length);

                        mydata.JournalVolume = journal_volume;
                    }
                    
                    //DAY

                    int idx_journal_day = journal.IndexOf("<Day>");

                    if (idx_journal_day != -1)
                    {
                        int idx_journal_day_end = journal.IndexOf("</Day>", idx_journal_day);

                        string journal_day = journal.Substring(idx_journal_day + "<Day>".Length, idx_journal_day_end - idx_journal_day - "<Day>".Length);

                        mydata.JournalDay = journal_day;
                    }

                    //MONTH

                    int idx_journal_month = journal.IndexOf("<Month>");

                    if (idx_journal_month != -1)
                    {
                        int idx_journal_month_end = journal.IndexOf("</Month>", idx_journal_month);

                        string journal_month = journal.Substring(idx_journal_month + "<Month>".Length, idx_journal_month_end - idx_journal_month - "<Month>".Length);

                        mydata.JournalMonth = journal_month;
                    }

                    //YEAR

                    int idx_journal_year = journal.IndexOf("<Year>");

                    if (idx_journal_year != -1)
                    {
                        int idx_journal_year_end = journal.IndexOf("</Year>", idx_journal_year);

                        string journal_year = journal.Substring(idx_journal_year + "<Year>".Length, idx_journal_year_end - idx_journal_year - "<Year>".Length);

                        mydata.JournalYear = journal_year;
                    }

                }

                // JOURNAL

                int idx_authorlist = xml.IndexOf("<AuthorList");

                if (idx_authorlist != -1)
                {
                    int idx_authorlist_end = xml.IndexOf("</AuthorList>", idx_authorlist);

                    string authorlist = xml.Substring(idx_authorlist + "<AuthorList".Length, idx_authorlist_end - idx_authorlist - "<AuthorList".Length);

                    int idx_author = authorlist.IndexOf("<Author");

                    while (idx_author != -1)
                    {
                        author Author = new author();

                        int idx_author_end = authorlist.IndexOf("</Author>", idx_author);

                        string author = authorlist.Substring(idx_author + "<Author".Length, idx_author_end - idx_author - "<Author".Length);

                        int idx_forename = author.IndexOf("<ForeName>");

                        if (idx_forename != -1)
                        {
                            int idx_forename_end = author.IndexOf("</ForeName>", idx_forename);

                            string forename = author.Substring(idx_forename + "<ForeName>".Length, idx_forename_end - idx_forename - "<ForeName>".Length);

                            Author.ForeName = forename;
                        }

                        int idx_lastname = author.IndexOf("<LastName>");

                        if (idx_lastname != -1)
                        {
                            int idx_lastname_end = author.IndexOf("</LastName>", idx_lastname);

                            string lastname = author.Substring(idx_lastname + "<LastName>".Length, idx_lastname_end - idx_lastname - "<LastName>".Length);

                            Author.LastName = lastname;
                        }

                        int idx_initials = author.IndexOf("<Initials>");

                        if (idx_initials != -1)
                        {
                            int idx_initials_end = author.IndexOf("</Initials>", idx_initials);

                            string initials = author.Substring(idx_initials + "<Initials>".Length, idx_initials_end - idx_initials - "<Initials>".Length);

                            Author.Initials = initials;
                        }

                        mydata.AuthorList.Add(Author);

                        idx_author = authorlist.IndexOf("<Author",idx_author + 1);
                    }

                }

                // INSERT DATA INTO SQL SERVER

                //SqlConnection conn = new SqlConnection("Data source=W10SQLDB; Database=RETINA_REFERENCES;User Id=sa;Password=H@rdQu@le");

                //conn.Open();

                //string query = "INSERT INTO ARTICLES (PMIDv1,PMIDv2,PMIDv3,ArticleTitle,Abstract,PubDate_Day,PubDate_Month,PubDate_Year,OtherAbstract,JournalTitle,JournalIssue,JournalVolume,JournalDay,JournalMonth,JournalYear)";
                //query += " VALUES (@PMIDv1,@PMIDv2,@PMIDv3,@ArticleTitle,@Abstract,@PubDate_Day,@PubDate_Month,@PubDate_Year,@OtherAbstract,@JournalTitle,@JournalIssue,@JournalVolume,@JournalDay,@JournalMonth,@JournalYear)";

                //SqlCommand myCommand = new SqlCommand(query, conn);

                //myCommand.Parameters.AddWithValue("@PMIDv1", mydata.PMIDv1);
                //myCommand.Parameters.AddWithValue("@PMIDv2", mydata.PMIDv2);
                //myCommand.Parameters.AddWithValue("@PMIDv3", mydata.PMIDv3);

                //myCommand.Parameters.AddWithValue("@ArticleTitle", mydata.ArticleTitle);
                //myCommand.Parameters.AddWithValue("@Abstract", mydata.Abstract);

                //myCommand.Parameters.AddWithValue("@PubDate_Day", mydata.PubDate_Day);
                //myCommand.Parameters.AddWithValue("@PubDate_Month", mydata.PubDate_Month);
                //myCommand.Parameters.AddWithValue("@PubDate_Year", mydata.PubDate_Year);

                //myCommand.Parameters.AddWithValue("@OtherAbstract", mydata.OtherAbstract);

                //myCommand.Parameters.AddWithValue("@JournalTitle", mydata.JournalTitle);
                //myCommand.Parameters.AddWithValue("@JournalIssue", mydata.JournalIssue);
                //myCommand.Parameters.AddWithValue("@JournalVolume", mydata.JournalVolume);
                //myCommand.Parameters.AddWithValue("@JournalDay", mydata.JournalDay);
                //myCommand.Parameters.AddWithValue("@JournalMonth", mydata.JournalMonth);
                //myCommand.Parameters.AddWithValue("@JournalYear", mydata.JournalYear);

                //myCommand.ExecuteNonQuery();

                //conn.Close();

                for (int iAuthor = 0; iAuthor < mydata.AuthorList.Count; iAuthor++)
                {
                    SqlConnection AuthorConn = new SqlConnection("Data source=W10SQLDB; Database=RETINA_REFERENCES;User Id=sa;Password=H@rdQu@le");

                    AuthorConn.Open();

                    string queryAuthor = "INSERT INTO AUTHORS (PMIDv1,ID,Initials,ForeName,LastName)";

                    queryAuthor += " VALUES (@PMIDv1,@ID,@Initials,@ForeName,@LastName)";

                    SqlCommand myCommandAuthor = new SqlCommand(queryAuthor, AuthorConn);

                    myCommandAuthor.Parameters.AddWithValue("@PMIDv1", mydata.PMIDv1);
                    myCommandAuthor.Parameters.AddWithValue("@ID", Convert.ToString(iAuthor + 1));
                    myCommandAuthor.Parameters.AddWithValue("@Initials", mydata.AuthorList[iAuthor].Initials);
                    myCommandAuthor.Parameters.AddWithValue("@ForeName", mydata.AuthorList[iAuthor].ForeName);
                    myCommandAuthor.Parameters.AddWithValue("@LastName", mydata.AuthorList[iAuthor].LastName);

                    int output = myCommandAuthor.ExecuteNonQuery();

                    AuthorConn.Close();

                }
                
            }



        }
    }

    public class myData
    {

        public string PMIDv1 = string.Empty;

        public string PMIDv2 = string.Empty;

        public string PMIDv3 = string.Empty;

        public string Abstract = string.Empty;

        public string ArticleDate_Day = string.Empty;

        public string ArticleDate_Month = string.Empty;

        public string ArticleDate_Year = string.Empty;

        public string PubDate_Day = string.Empty;

        public string PubDate_Month = string.Empty;

        public string PubDate_Year = string.Empty;

        public string ArticleTitle = string.Empty;

        public string OtherAbstract = string.Empty;

        public string JournalTitle = string.Empty;

        public string JournalIssue = string.Empty;

        public string JournalVolume = string.Empty;

        public string JournalDay = string.Empty;

        public string JournalMonth = string.Empty;

        public string JournalYear = string.Empty;

        public List<author> AuthorList = new List<author>();

    }

    public class author
    {
        public string ForeName = string.Empty;

        public string LastName = string.Empty;

        public string Initials = string.Empty;
    }
}
