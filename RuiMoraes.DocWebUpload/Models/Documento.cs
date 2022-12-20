using System.ComponentModel.DataAnnotations;

namespace RuiMoraes.DocWebUpload.Models
{
    public class Documento : BaseEntidade
    {
        [Display(Name = "Nome do Documento")]
        public string NomeDocumento { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Caminho")]
        public string Caminho { get; set; }
       
        [Display(Name = "Status do Documento")]

        public EnumStatusDoc StatusDoc { get; set; }
    }
}
