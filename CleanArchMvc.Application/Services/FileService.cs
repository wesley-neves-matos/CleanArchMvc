using CleanArchMvc.Application.Interfaces;
using System.Linq;

namespace CleanArchMvc.Application.Services
{
    public class FileService
    {
        public string FolderPath { get; private set; }
        public string? OldName { get; private set; }
        public string CurrentName { get; private set; }

        public FileService(string folderPath, string currentName)
        {
            ValidateFolderPath(folderPath);
            ValidateCurrentName(currentName);
        }

        public FileService(string folderPath, string currentName, string oldName)
        {
            ValidateFolderPath(folderPath);
            ValidateCurrentName(currentName);
            ValidateOldName(oldName);
        }


        public void SaveCurrentFile(string FileToCopy)
        {
            DeleteOldFileIfExists();

            DeleteCurrentFileIfExists();

            CopyFileForCurrentFile(FileToCopy);
        }

        public void DeleteCurrentFile()
        {
            DeleteCurrentFileIfExists();
        }

        public void UpdateFolderPath(string newFolderPath)
        {
            ValidateFolderPath(newFolderPath);
        }

        public void UpdateCurrentName(string currentName)
        {
            ValidateCurrentName(currentName);
        }

        public void UpdateOldName(string oldName)
        {
            ValidateOldName(oldName);
        }

        public string CurrentFullPath()
        {
            return $@"{FolderPath}\{CurrentName}";
        }

        public string OldFullPath()
        {
            if (OldName == null)
            {
                throw new ArgumentNullException(nameof(OldName));
            }
            return $@"{FolderPath}\{OldName}";
        }


        private void ValidateFolderPath(string folderPath)
        {
            ValidateInvalidCharsInFolderPath(folderPath);
            ValidateIfFolderPathExists(folderPath);
            FolderPath = folderPath;
        }

        private void ValidateCurrentName(string currentName)
        {
            ValidateInvalidCharsInFileName(currentName);
            CurrentName = currentName;
        }

        private void ValidateOldName(string oldName)
        {
            ValidateInvalidCharsInFileName(oldName);
            OldName = oldName;
        }

        private void DeleteFile(string fileNameWithExtension)
        {
            if (File.Exists(fileNameWithExtension))
            {
                try
                {
                    File.Delete(fileNameWithExtension);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void CopyFileForCurrentFile(string FileToCopy)
        {
            try
            {
                File.Copy(FileToCopy, CurrentFullPath());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DeleteCurrentFileIfExists()
        {
            try
            {
                DeleteFile(CurrentFullPath());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DeleteOldFileIfExists()
        {
            if (OldName != null)
            {
                try
                {
                    DeleteFile(OldFullPath());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private static void ValidateIfFolderPathExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException("Folder path not found!");
            }
        }

        private static void ValidateInvalidCharsInFolderPath(string folderPath)
        {
            var invalidPathChars = Path.GetInvalidPathChars();

            foreach (var invalidPathChar in invalidPathChars)
            {
                if (folderPath.Contains(invalidPathChar))
                {
                    throw new ArgumentException($"The folder name can'nt contais the follow chars: [ {string.Join(", ", invalidPathChars)} ]");
                }
            }
        }

        private static void ValidateInvalidCharsInFileName(string fileName)
        {
            var invalidPathChars = Path.GetInvalidFileNameChars();

            if (fileName != null)
            {
                foreach (var invalidPathChar in invalidPathChars)
                {
                    if (fileName.Contains(invalidPathChar))
                    {
                        throw new ArgumentException($"The folder name can'nt contais the follow chars: {string.Join(", ", invalidPathChars)}");
                    }
                }
            }
        }
    }
}
