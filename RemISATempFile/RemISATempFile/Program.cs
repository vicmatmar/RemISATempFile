using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace RemISATempFile
{
    class Program
    {
        const string _isa_util_path = @"C:\Program Files (x86)\Ember\ISA3 Utilities\bin";
        const string _temp_file_name = "em3xx_load_temp_patch_file_*.s37";

        static void Main(string[] args)
        {

            string[] removed_files = CleanupTempPatchFile();
            foreach (string removed_fle in removed_files)
                Console.WriteLine("Removed : " + removed_fle);
        }

        public static string[] CleanupTempPatchFile()
        {
            List<string> removed_list = new List<string>();

            string path = _isa_util_path;
            string[] removed_files = removeFilesFromFolder(path, _temp_file_name);

            path = AppDomain.CurrentDomain.BaseDirectory;
            removed_files = removeFilesFromFolder(path, _temp_file_name);
            removed_list.AddRange(removed_files);

            path = Path.GetTempPath();
            removed_files = removeFilesFromFolder(path, _temp_file_name);
            removed_list.AddRange(removed_files);

            path = Environment.GetEnvironmentVariable("LOCALAPPDATA");
            path = Path.Combine(path, @"VirtualStore\Program Files (x86)\Ember\ISA3 Utilities\bin");
            removed_files = removeFilesFromFolder(path, _temp_file_name);
            removed_list.AddRange(removed_files);

            path = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), @"AppData\Local\Apps");
            removed_files = removeFilesFromFolder(path, _temp_file_name, SearchOption.AllDirectories);
            removed_list.AddRange(removed_files);

            return removed_list.ToArray();
        }

        static string[] removeFilesFromFolder(string folder, string file_find_name, SearchOption options = SearchOption.TopDirectoryOnly)
        {
            List<string> removed_list = new List<string>();
            if (Directory.Exists(folder))
            {
                string[] files = Directory.GetFiles(folder, file_find_name, options);
                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                        removed_list.Add(file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return removed_list.ToArray();
        }


    }
}
