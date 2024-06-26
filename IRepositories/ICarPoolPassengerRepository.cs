﻿using DTO.CarPoolPassenger;
using DTO.Pagination;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICarPoolPassengerRepository
    {
        public Task<int> CreateCarPoolPassengerAsync(CreateCarPoolPassengerDTO createCarPoolPassengerDTO);
        public Task<int> DeleteCarPoolPassengerByIdAsync(int carpoolPassengerId);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionLocalizationAsync(int localizationID);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionDateAsync(DateTime dateTime);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByUserAsync(string userID);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByCarPoolAsync(int carPoolID);
        public Task<List<GetOneCarPoolPassengerDTO>> GetAllPassengersAsync(PageForkDTO pageForkDTO);
        public Task<GetOneCarPoolPassengerDTO> GetPassengerByIdAsync(int carPoolPassengerID);
        public Task<CarPoolPassenger?> GetCarpoolPassengerTypeAsync(int carpoolPassengerId);
    }
}
