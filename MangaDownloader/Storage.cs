using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MangaDownloader
{
    public static class Storage
    {
        public static void SaveImage(Bitmap bitmap, string filePath)
        {
            try
            {
                bitmap.Save(filePath);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                throw ex;
            }
        }

        //public static async Task SaveImageAsync(Bitmap bitmap, string filePath)
        //{
        //    await Task.Run(() =>
        //    {
        //        try
        //        {
        //            bitmap.Save(filePath);
        //        }
        //        catch (ArgumentNullException ex)
        //        {
        //            throw ex; // handle // onEvent
        //        }
        //        catch (System.Runtime.InteropServices.ExternalException ex)
        //        {
        //            throw ex; // handle // onEvent
        //        }
        //    });
        //}

        public static void SaveMangaToFile(MangaInfo mangaInfo)
        {
            string filePath = string.Format("{0}.dat", mangaInfo.Title);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, mangaInfo);
                }
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        //public static async Task SaveMangaToFileAsync(MangaInfo mangaInfo)
        //{
        //    await Task.Run(() =>
        //    {
        //        try
        //        {
        //            SaveMangaToFile(mangaInfo);
        //        }
        //        catch (ArgumentException ex)
        //        {
        //            throw ex; // handle // onEvent
        //        }
        //        catch (IOException ex)
        //        {
        //            throw ex; // handle // onEvent
        //        }
        //    });
        //}
    }
}
