using DaliFood.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    public static class Files
    {
        public static bool DeletePhotos(UnitOfWork unitofwork,IEnumerable<Photo> photos,PhotoFor part)
        {
            try
            {
                foreach (var photo in photos)
                {
                    unitofwork.PhotoRepository.Delete(photo);
                    var savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", part.PhotoSavedAddress, photo.Id.ToString() + photo.Extention);
                    System.IO.File.Delete(savepath);
                }
                unitofwork.PhotoRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        public static bool ImportPhotos(UnitOfWork unitofwork, IEnumerable<IFormFile> ImagesUpload, Product product, PhotoFor part)
        {
            try
            {
                foreach (var ImageUpload in ImagesUpload)
                {
                    var photo = new Models.Photo() { PartId = part.Id, ItemId = product.Id, Extention = Path.GetExtension(ImageUpload.FileName) };
                    unitofwork.PhotoRepository.Create(photo);
                    int photoId = 0;
                    if (!unitofwork.PhotoRepository.GetAll().Any())
                    {
                        unitofwork.PhotoRepository.Save();
                        photoId = unitofwork.PhotoRepository.GetAll().Last().Id;

                    }
                    else
                    {
                        photoId = unitofwork.PhotoRepository.GetAll().Last().Id + 1;
                    }

                    var savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", part.PhotoSavedAddress, photoId.ToString() + photo.Extention);
                    using (var Filestream = new FileStream(savepath, FileMode.Create))
                    {
                        ImageUpload.CopyTo(Filestream);
                    }
                    unitofwork.PhotoRepository.Save();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
