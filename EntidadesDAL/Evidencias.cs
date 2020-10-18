namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Evidencias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Animal { get; set; }

        public decimal ValorBase { get; set; }
        public decimal CalculoTotalDentes { get; set; }
        public string Motivo { get; set; }


    }
}
