using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using System.Windows.Forms;
namespace MyDVLD_Win_Form.GloablClasses
{
    internal class clsUtil
    {
        private static Guid GenerateGUID()
        {
            Guid guid = new Guid();

            return guid;
        }
        private static string _ReplacFileNameWithGuid(string PathImage)
        {
            FileInfo FileInfo = new FileInfo(PathImage);
            string extn = FileInfo.Extension;
            return GenerateGUID().ToString() + extn;
        }
        private static bool _CreateFolderImageIfDeosNotExist(string FolderPath)
        {
            if(!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (IOException ex)
                {

                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }
            return true;
           
        }
        public static bool CopyImageToProjectImageFolder(ref string PathImage)
        {
            string FolderPath = $@"C:\Users\Asus\Desktop\DVLD_Muhammad";
            if (!_CreateFolderImageIfDeosNotExist(FolderPath))
            {
                return false;
            }
            //ننشئ (GUID) و ندمج معه صيغة الصورة
            string destinationFile = FolderPath + _ReplacFileNameWithGuid(PathImage);
            try
            {
              //ننسخ الصورة في ملف المشروع
                File.Copy(PathImage, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            PathImage = destinationFile;
            return true;
        }
    }
}
