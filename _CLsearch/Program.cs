/* Created By:       Joshua Granger
* Date Created:     October 21, 2017
* 
* Instructions for use:
*   1. Copy the executable into the directory that contains the CL files to search
*   2. Open the command prompt
*   3. Type the following:
*       C:\path\to\this\exe\_CLsearch.exe TAG1 TAG2 TAG3
*       
* Example input:
*   C:\Users\Joshua\Desktop\CLDIRECTORY\_CLsearch.exe 20BLSOL 21BLSOL
*   
* Example output:
*   "20BLSOL"
*       FILE: 20BLSOL.CL    LINE: 8
*       FILE: 20BLSOL.CL    LINE: 60
*       FILE: 20BLSOL.CL    LINE: 78
*          
*   "21BLSOL"
*       FILE: 21BLSOL.CL    LINE: 8
*       FILE: 21BLSOL.CL    LINE: 60
*       FILE: 21BLSOL.CL    LINE: 78
*          
* Note:
*   The results of this search are also logged into a file named _CLsearch.txt
*       _CLsearch.txt is created in the same directory as _CLsearch.exe
*       _CLsearch.txt is OVERWRITTEN every time this application is executed*/

using System;
using System.Collections.Generic;
using System.IO;

namespace _CLsearch
{
    class Program
    {
        static void Main(string[] args)
        {
            // Gather some information
            string[] tags = args;
            for (int i = 0; i < tags.Length; i++)
                tags[i] = tags[i].Replace(",", "");

            string exe_dir = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8);
            string exe_name = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string cl_dir = exe_dir.Substring(0, exe_dir.IndexOf(exe_name));
            string[] cl_dir_files = Directory.GetFiles(cl_dir);
            List<String> output = new List<string>();

            // If the user did not provide search terms (or "tags"), stop here
            if(tags.Length == 0)
            {
                output.Add("Search not completed: no tags to search for.");
            }

            // If the user provided search terms (or "tags"), continue
            else
            {
                // Check the directory that the .exe resides in to determine
                // whether or not there are ".CL" files in the same directory
                bool cl_files_present = false;
                foreach (string file in cl_dir_files)
                {
                    if (file.ToUpper().Contains(".CL"))
                    {
                        cl_files_present = true;
                    }
                }

                // If there are no ".CL" files to search, stop here
                if (!cl_files_present)
                {
                    output.Add("Search not completed: no \".CL\" files are present in this directory.");
                }

                // If there are ".CL" files in the directory, search for the terms
                else
                {
                    foreach (string tag in tags)
                    {
                        // First, write the tag
                        output.Add(string.Format("\"{0}\"", tag.ToUpper()));
                        foreach (string file in cl_dir_files)
                        {
                            // Find the next ".CL" file and open
                            if (file.ToUpper().Contains(".CL"))
                            {
                                string temp = "";
                                List<string> lines = new List<string>();
                                using (StreamReader fs = new StreamReader(file))
                                {
                                    while ((temp = fs.ReadLine()) != null)
                                        lines.Add(temp);
                                }
                                // Check to see if our tag is in the file
                                int line_num = 1;
                                foreach (string line in lines)
                                {
                                    if (line.ToUpper().Contains(tag.ToUpper()))
                                    {
                                        output.Add("\tFILE: " + (file.Replace(cl_dir, "") +
                                            "                ").Substring(0, 16) +
                                            "\tLINE: " + line_num.ToString());
                                    }
                                    line_num++;
                                }
                            }
                        }

                        // If we get to the end of our files and no new lines were added, 
                        // then the tag was not found
                        if (output[output.Count-1].Contains("\"" + tag.ToUpper() + "\""))
                        {
                            output.Add("\t**No matches found**");
                        }

                        output.Add("");
                    }
                }
            }

            // Write the results to console and file
            using (StreamWriter sw = new StreamWriter(cl_dir + "_CLsearch.txt", false))
            {
                for (int x = 0; x < output.Count; x++)
                {
                    if (x != (output.Count - 1))
                    {
                        sw.WriteLine(output[x]);
                        Console.WriteLine(output[x]);
                    }
                    else
                    {
                        sw.Write(output[x]);
                        Console.Write(output[x]);
                    }
                }
            }
        }
    }
}
