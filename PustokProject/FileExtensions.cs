using System;

public static class FileExtensions
{
	public const string ImageFolderPath = "wwwroot/bookimages";



	public async static Task<string> SaveToRootWithUniqueNameAsync(this IFormFile file)
	{

		var rootPath = Directory.GetCurrentDirectory();
		var imagePath = Path.Combine(rootPath, ImageFolderPath);
		var fileName = Guid.NewGuid().ToString()+file.FileName;

		var imageFile = File.Create(Path.Combine(imagePath, fileName));

		await file.CopyToAsync(imageFile);

		imageFile.Close();
		return fileName;
	}
	
}
