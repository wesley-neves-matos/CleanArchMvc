namespace CleanArchMvc.Application.DTOs
{
    public static class ProductDTOExtensions
    {
        public static string ImageFullPath(this ProductDTO productDTO, string folderPath)
        {
            return $@"{folderPath}\{productDTO.ImageName()}";
        }

        public static bool ImageExists(this ProductDTO productDTO, string folderPath)
        {
            return File.Exists(productDTO.ImageFullPath(folderPath));
        }

        public static void DeleteImage(this ProductDTO productDTO, string folderPath)
        {
            File.Delete(productDTO.ImageFullPath(folderPath));
        }

        public static void UpdateImage(this ProductDTO productDTO, string destinationFolderPath)
        {
            if (productDTO.ImageExists(destinationFolderPath))
            {
                File.Delete(destinationFolderPath);
            }
            File.Copy(destinationFolderPath, productDTO.ImageFullPath(destinationFolderPath));
        }
    }
}
