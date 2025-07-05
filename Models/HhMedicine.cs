using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhMedicine
{
    public int MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public string? MedicineGenericName { get; set; }

    public string? MedicineBrand { get; set; }

    public string? MedicineDescription { get; set; }

    public string? MedicineDosage { get; set; }

    public decimal? MedicinePrice { get; set; }

    public int? CategoryId { get; set; }

    public int? UnitId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual HhMedicineCategory? Category { get; set; }

    [JsonIgnore]
    public virtual HhMedicineUnit? Unit { get; set; }
}
