﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_API.Models;

public partial class Person
{
    public int IdPerson { get; set; }

    public string NamePerson { get; set; } = null!;

    public string LastNamePerson { get; set; } = null!;

    public string? SecondLastNamePerson { get; set; }

    public double? AgePerson { get; set; }

    public string? PhoneNumberPerson { get; set; }

    public string? AddressPerson { get; set; }

    public string? NationalityPerson { get; set; }

    public byte? StatePerson { get; set; }

    public string? IdentificationPerson { get; set; }

    public string? EmailPerson { get; set; }

    public int? IdCompanyFk { get; set; }

    [Column("UUID_person")]
    public Guid PersonUUID { get; set; } // Mapeo de la columna UUID_person// Nuevo campo UUID

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual Company? IdCompanyFkNavigation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
