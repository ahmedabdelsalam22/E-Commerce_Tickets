﻿using eTickets.Data.Services.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Data.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        IActorRepository actorRepository { get; }
        IProducerRepository producerRepository { get; }
        ICinemaRepository cinemaRepository { get; }
        ICategoryRepository categoryRepository { get; }
        IMovieRepository movieRepository { get; }
    }
}
