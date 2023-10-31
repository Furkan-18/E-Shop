using System.ComponentModel.DataAnnotations;

namespace E_Shop.WebUI.Areas.Admin.Models
{
    public class CategoryFormViewModel
    {
    

        public int Id { get; set; }//Bu model ile formdan hem ekleme hem güncelleme yapılaceağı için Id bilgiside taşınmalı.
        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori Adı alanını doldurmak zorunludur.")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        [MaxLength(1000)]
        public string? Description { get; set; }
        // ? -> nullable - boş gönderilebilir.





    }
}
