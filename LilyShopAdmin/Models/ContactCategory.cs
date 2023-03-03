﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LilyShopAdmin.Models
{
    [Table("ContactCategory")]
    public partial class ContactCategory
    {
        public ContactCategory()
        {
            Contacts = new HashSet<Contact>();
        }

        [Key]
        public int ContactCategoryID { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Thumb { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }

        [ForeignKey("CreateBy")]
        [InverseProperty("ContactCategories")]
        public virtual Account CreateByNavigation { get; set; }
        [InverseProperty("ContactCategory")]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}