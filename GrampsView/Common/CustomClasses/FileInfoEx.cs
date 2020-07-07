namespace GrampsView.Common
{
    using System.IO;

    public class FileInfoEx : CommonBindableBase
    {
        private FileInfo _FInfo;

        public FileInfoEx()
        {
            FInfo = null;
        }

        public FileInfoEx(FileInfo argFInfo)
        {
            FInfo = argFInfo;
        }

        public bool Exists
        {
            get
            {
                if (FInfo != null)
                {
                    return FInfo.Exists;
                }

                return false;
            }
        }

        public FileInfo FInfo
        {
            get
            {
                return _FInfo;
            }
            set
            {
                SetProperty(ref _FInfo, value);
            }
        }

        public bool Valid
        {
            get
            {
                return (!(FInfo == null) && (FInfo.Exists));
            }
        }
    }
}