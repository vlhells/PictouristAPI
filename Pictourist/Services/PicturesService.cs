using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using System;
using System.Drawing;
using System.Security.Cryptography;

namespace PictouristAPI.Services
{
	public class PicturesService : IPicturesService
	{
		private PictouristContext _db;
		private IConfiguration _appConfiguration;

		public PicturesService(PictouristContext db, IConfiguration appConfiguration)
		{
			_db = db;
			_appConfiguration = appConfiguration;
		}

		// TODO: not correct work.
		private bool IsFileAnImage(string filePath)
		{
			Image image;
			try
			{
				image = Image.FromFile(filePath);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public async Task<List<bool>> LoadPicture(FormFileCollection files, string loaderGuid)
		{
			List<bool> resultsOfLoadings = new List<bool>();
			resultsOfLoadings.Clear();
			if (files.Count != 0)
			{
				var storageFolder = _appConfiguration.GetValue<string>("Config:PicturesStorage");
				var uploadPath = $"{Directory.GetCurrentDirectory()}{storageFolder}";
				// Создаем папку для хранения файлов:
				Directory.CreateDirectory(uploadPath);

				User user = null;
				if (loaderGuid != null)
					user = await _db.Users.FirstOrDefaultAsync(u => u.Id == loaderGuid);
				if (user != null)
				{
					// File loadin:
					foreach (var file in files)
					{
						string filePath = $"{uploadPath}/{file.FileName}";
						Random r = new Random();
						while (File.Exists(filePath))
						{
							filePath = $"{uploadPath}/{file.FileName}";
							int i = r.Next(1000001);
							filePath = $"{uploadPath}/{i}_{file.FileName}";
						}

						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							// TODO: check if this photo (pixels) is already exists in storage:
							// if not:
							await file.CopyToAsync(fileStream);

							//else: add this record this-pic-id--this-user-id.
							//var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == loaderGuid);
							//var picture = await _db.Pictures.FirstOrDefaultAsync(p => p.PathToFile == $"./{file.FileName}");
							//picture.Users.Add(user);
							//await _db.SaveChangesAsync();
						}
						if (IsFileAnImage(filePath))
						{
							Picture p = new Picture
							{
								//FirstLoaderGuid = loaderGuid,
								PathToFile = $"./{file.FileName}"
							};
							p.Users.Add(user);
							await _db.Pictures.AddAsync(p);
							await _db.SaveChangesAsync();
							resultsOfLoadings.Add(true);
						}
						else
						{
							System.IO.File.Delete(@filePath);
							resultsOfLoadings.Add(false);
						}
					}
				}
			}
			return resultsOfLoadings;
		}

	}
}
