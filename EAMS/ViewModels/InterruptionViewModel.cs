
using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace EAMS.ViewModels
{
    public class InterruptionViewModel
    {

        [Required(ErrorMessage = "BoothMaster Id is required")]
        public string boothMasterId { get; set; }

      
        public string? stopTime { get; set; }
        public string? ResumeTime { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get; set; }

      
        public string? newCU { get; set; }
        public string? newBU { get; set; }
        public string? oldCu { get; set; }
        public string? oldBu { get; set; }
        
    }
}

