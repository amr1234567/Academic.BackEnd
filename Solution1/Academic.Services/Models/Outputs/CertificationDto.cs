using Academic.Core.Entities;
using Academic.Core.Entities.ManyToManyEntities;
using Academic.Core.Identitiy;
using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Outputs
{
    public class CertificationDto
    {
        public int UserId { get; set; }
        public int PathTaskId { get; set; }
        public double PathTaskPoints { get; set; }
        public double MinPointsToCertify { get; set; }
        public string Description { get; set; }

        public bool HasCertification { get; set; }
        public double Score { get; set; }

        public CertificationDto()
        {
            
        }

        public CertificationDto(PathTaskUsers pathTask)
        {
            UserId = pathTask.UserId;
            PathTaskId = pathTask.PathTaskId;
            PathTaskPoints = pathTask.PathTask.TotalPoints;
            MinPointsToCertify = pathTask.PathTask.MinPointsToCertify;
            Description = pathTask.PathTask.Description;
            HasCertification = pathTask.HasCertification;
            Score = pathTask.Score;
        }
    }
}