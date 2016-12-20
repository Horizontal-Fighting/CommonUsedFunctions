using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvFileProcessor.Model
{
    public class FileRecord
    {
        public FileRecord(string curLineStr)
        {
            string[] resArr = curLineStr.Split(',');
            this.ID = Convert.ToInt32(resArr[0]);
            this.FullFileName = resArr[1];
            this.Extension = resArr[2];
            this.Folder = resArr[3];
            this.Size = Convert.ToInt64(resArr[4]);
            this.ModifiedDate = Convert.ToDateTime(resArr[5]);
            this.CreatedDate = Convert.ToDateTime(resArr[6]);
            this.FullFileName = resArr[7];
        }

        public FileRecord()
        {
        }

        public Int64 GetSize(string _lineStr)
        {
            var arr = _lineStr.Split(',');
            try
            {
                Int64 size = Convert.ToInt64(arr[4].Trim());
                return size;
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int ID { get; set; }
        public string FullFileName { get; set; }
        public string Extension { get; set; }
        public string Folder { get; set; }
        public Int64 Size { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Other { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ID.ToString() + ",");
            sb.Append(FullFileName + ",");
            sb.Append(Extension + ",");
            sb.Append(Size.ToString() + ",");
            sb.Append(CreatedDate.ToString() + ",");
            sb.Append(ModifiedDate.ToString() + ",");
            sb.Append(Other.ToString());
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return 13 * ID.GetHashCode() + 17 * FullFileName.GetHashCode() + 19 * Size.GetHashCode();
        }

    }
}
