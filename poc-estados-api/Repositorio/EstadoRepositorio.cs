﻿using poc_estados_api.Data;
using poc_estados_api.Models;
using poc_estados_api.Repositorio.IRepositorio;

namespace poc_estados_api.Repositorio;

public class EstadoRepositorio: IEstadoRepositorio
{
    private readonly ApplicationDbContext _bd;
    
    public EstadoRepositorio(ApplicationDbContext bd) => _bd = bd;
    
    public ICollection<Estado> GetEstados()
    {
        return _bd.Estados.OrderBy(c => c.Nombre).ToList();
    }

    public Estado GetEstadoById(int IdEstado)
    {
        return _bd.Estados.FirstOrDefault(c => c.IdEstado == IdEstado);
    }

    public bool ExisteEstado(int IdEstado)
    {
        return _bd.Estados.Any(e => e.IdEstado == IdEstado);
    }

    public bool ExisteEstado(string Nombre)
    {
        bool valor = _bd.Estados.Any(e => e.Nombre.ToLower().Trim() == Nombre.ToLower().Trim());
        return valor;
    }

    public bool CrearEstado(Estado estado)
    {
        estado.Creado = DateTime.Now;
        _bd.Estados.Add(estado);
        return GuardarEstado();
    }

    public bool ActualizarEstado(Estado estado)
    {
        var estadoExiste = _bd.Estados.Find(estado.IdEstado);
        if (estadoExiste != null)
        {
            _bd.Entry(estadoExiste).CurrentValues.SetValues(estado);
            _bd.Entry(estadoExiste).Property(e => e.Creado).IsModified = false; // ✅ Evita modificar la fecha de creación
        }
        else
        {
            _bd.Estados.Update(estado);
        }

        return GuardarEstado();
    }


    public IEnumerable<(int IdEstadoDesde, string Accion, int IdEstadoHasta)> GetTransiciones()
    {
        var transiciones = _bd.AccionesEstado
            .Join(_bd.Acciones, 
                ae => ae.IdAccionEstado, 
                a => a.IdAccion, 
                (ae, a) => new 
                {
                    ae.IdEstadoDesde,
                    Accion = a.Descripcion ?? "sin accion", // Asegurar que nunca sea NULL
                    ae.IdEstadoHasta
                })
            .ToList();

        // 🔹 Depurar valores en consola
        Console.WriteLine($"🔍 Transiciones obtenidas: {transiciones.Count}");
        foreach (var t in transiciones)
        {
            Console.WriteLine($"{t.IdEstadoDesde} -> {t.IdEstadoHasta} | Acción: '{t.Accion}'");
        }

        return transiciones.Select(t => (t.IdEstadoDesde, t.Accion, t.IdEstadoHasta));
    }


    public bool BorrarEstado(Estado estado)
    {
        _bd.Estados.Remove(estado);
        return GuardarEstado();
    }

    public bool GuardarEstado()
    {
        return _bd.SaveChanges() > 0 ? true : false;
    }
}