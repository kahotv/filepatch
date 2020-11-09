using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvfpatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "file patch";

            Console.Write("原始文件：");
            string filesource = Console.ReadLine();
            Console.Write("补丁文件：");
            string filepatch = Console.ReadLine();

            try
            {
                var source = File.ReadAllBytes(filesource);
                var patch = File.ReadAllBytes(filepatch);

                Console.WriteLine("原始文件长度：" + source.Length);
                Console.WriteLine("补丁文件长度：" + patch.Length);

                Console.WriteLine("开始补丁...");
                var target = xdelta3.net.Xdelta3Lib.Decode(source, patch);
                string filetarget = Path.Combine(Path.GetDirectoryName(filesource), DateTime.Now.ToString("yyyyMMddHHmmss") + ".file");
                using (FileStream fs = new FileStream(filetarget, FileMode.Create))
                {
                    fs.Write(target.ToArray(), 0, target.Length);
                }

                Console.WriteLine("输出文件：" + filetarget);
            }
            catch (Exception e)
            {
                Console.WriteLine("异常：" + e.Message);
            }

            Console.WriteLine("处理完成，任意键退出");
            Console.ReadKey();
        }
    }
}
