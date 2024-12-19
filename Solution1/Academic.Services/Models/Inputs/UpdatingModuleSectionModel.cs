using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Academic.Services.Models.Inputs
{
    public class UpdatingModuleSectionModel
    {
        [AllowNull]
        [MaxLength(150)]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        [AllowNull]
        public string Body { get; set; }
    }
}