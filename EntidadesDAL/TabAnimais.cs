namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TabAnimais
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Animal { get; set; }

        [StringLength(1)]
        public string Sexo { get; set; }

        public int? CodPedido { get; set; }

        public int? QtdDentes { get; set; }

        public decimal? Peso { get; set; }

        public virtual TabPedido TabPedido { get; set; }
    }
}
