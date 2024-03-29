﻿using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; private set; }
    public Category(string name, string createdBy, string editedBy)
    {

        Name = name;
        Active = true;
        CreatedBy = createdBy;
        CreatedOn = DateTime.UtcNow;
        EditedBy = editedBy;
        EditedOn = DateTime.UtcNow;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
                    .IsNotNullOrEmpty(Name, "Name")
                    .IsGreaterOrEqualsThan(Name, 3, "Name")
                    .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                    .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active, string editedby) 
    {
        Active = active;
        Name = name;
        EditedBy = editedby;
        EditedOn = DateTime.UtcNow;

        Validate();
    }
}
