﻿namespace PizzaProjeto.Models;

public class PizzaModel
{
    public int Id { get; set; }
    public string Sabor { get; set; } = string.Empty;
    public string Capa { get; set; } = string.Empty;
    public double Valor { get; set; }
}