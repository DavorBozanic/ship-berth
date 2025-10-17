// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Application.DTOs;

namespace ShipBerth.Application.Interfaces
{
    /// <summary>
    /// Berth service interface.
    /// </summary>
    public interface IBerthService
    {
        /// <summary>
        /// Gets all berths asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<List<BerthDTO>> GetAllBerthsAsync();

        /// <summary>
        /// Searches the berths asynchronously.
        /// </summary>
        /// <param name="searchDto">The search dto.</param>
        /// <returns></returns>
        Task<List<BerthDTO>> SearchBerthsAsync(BerthSearchDTO searchDto);

        /// <summary>
        /// Gets the berth detail asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<BerthDetailDTO> GetBerthDetailAsync(int id);

        /// <summary>
        /// Determines whether [is berth available asynchronously] [the specified berth identifier].
        /// </summary>
        /// <param name="berthId">The berth identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        Task<bool> IsBerthAvailableAsync(int berthId, DateTime start, DateTime end);
    }
}
