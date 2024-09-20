using System.ComponentModel.DataAnnotations;

namespace TecnicosRegistro.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

	[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten caracteres especiales")]
	[Required(ErrorMessage = "Nombre obligatorio")]
	public string? Nombres { get; set; }

	[Required(ErrorMessage = "WhatsApp obligatorio")]
	[RegularExpression(@"^\d{7,15}$", ErrorMessage = "El número de WhatsApp debe contener entre 7 y 15 dígitos.")]
	public string? WhatssApp { get; set; }
}




