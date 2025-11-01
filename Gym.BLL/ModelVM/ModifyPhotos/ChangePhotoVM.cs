
using Microsoft.AspNetCore.Http;

namespace Gym.BLL.ModelVM.ModifyPhotos
{
    public class ChangePhotoVM
    {
        public int Id { get; set; }
        public string Image {  get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
