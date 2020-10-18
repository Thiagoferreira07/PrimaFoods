namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EvidenciaAnimais
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Animal { get; set; }

        [StringLength(1)]
        public string Sexo { get; set; }

        public int? CodPedido { get; set; }

        public decimal? Peso { get; set; }

        public virtual TabPedido TabPedido { get; set; }

        [StringLength(255)]
        public string Fornecedor { get; set; }
        public decimal ValorBase { get; set; }
        public decimal CalculoTotalDentes { get; set; }
        public string Motivo { get; set; }

        public int QtdAnimaisAbatidos { get; set; }
        public int QtdMachosAbatidos { get; set; }
        public int QtdFemeasAbatidas { get; set; }
        public int QtdAnimaisAgio { get; set; }
        public int QtdAnimaisDesagio { get; set; }
    }
}
