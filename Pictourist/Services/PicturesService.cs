using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using System;
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

		public async Task<bool> LoadPicture(FormFileCollection files, string loaderGuid)
		{
			if (files.Count != 0)
			{
				var storageFolder = _appConfiguration.GetValue<string>("Config:PicturesStorage");
				var uploadPath = $"{Directory.GetCurrentDirectory()}{storageFolder}";
				// Создаем папку для хранения файлов:
				Directory.CreateDirectory(uploadPath);

				// File loadin:
				foreach (var file in files)
				{
					string filePath = $"{uploadPath}/{file.FileName}";

					// TODO: check is file is image.
					// if success:
					// Сохраняем файл в папку:
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						// TODO: check if this photo is already exist in storage:
						// if not:
							await file.CopyToAsync(fileStream);
						    Picture p = new Picture
							{
								FirstLoaderGuid = loaderGuid,
								PathToFile = $"./{file.FileName}"
							};
							var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == loaderGuid);
							p.Users.Add(user);
							await _db.Pictures.AddAsync(p);
							await _db.SaveChangesAsync();

						//else: add this record this-pic-id--this-user-id.
						//var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == loaderGuid);
						var picture = await _db.Pictures.FirstOrDefaultAsync(p => p.PathToFile == $"./{file.FileName}");
						picture.Users.Add(user);
						await _db.SaveChangesAsync();
					}
				}
				return true;
			}

			return false;
		}

	}
}

