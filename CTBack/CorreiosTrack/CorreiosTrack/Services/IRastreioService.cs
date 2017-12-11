using CorreiosTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorreiosTrack.Services
{
    public interface IRastreioService
    {
        IQueryable<Rastreio> ConsultAllRastreios();

        Rastreio ConsultRastreio(long id);

        bool EditRastreio(long id, Rastreio rastreio);

        void CreateRastreio(Rastreio rastreio);

        Rastreio RemoveRastreio(long id);

        List<Rastreio> ConsultByStatusTest(long id);
    }
}
