using AutoMapper;
using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Exceptions;
using BookRentalSystem.DataAccess.Models;
using BookRentalSystem.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRentalSystem.Business.Services
{
    public class RentalRequestService(
        IRentalRequestRepository _rentalRequestRepository,
        IBookRepository _bookRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IRentalRequestService
    {
        public async Task<RentalRequestResponseDto> CreateRequestAsync(string? userId, CreateRentalRequestDto createRentalRequestDto)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException("User is not authenticated.");
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var book = await _bookRepository.GetByIdWithLockAsync(createRentalRequestDto.BookId);
                if (book == null)
                {
                    throw new NotFoundException("Book not found.");
                }

                if (book.AvailableCopies <= 0)
                {
                    throw new BadRequestException("Book is not available for rental.");
                }

                var hasPendingRequest = await _rentalRequestRepository.HasPendingRequestAsync(userId, createRentalRequestDto.BookId);
                if (hasPendingRequest)
                {
                    throw new ConflictException("You already have a pending rental request for this book.");
                }

                book.AvailableCopies -= 1;

                var rentalRequest = new RentalRequest
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    BookId = createRentalRequestDto.BookId,
                    Status = RentalRequestStatus.Pending,
                    Book = book
                };

                await _rentalRequestRepository.AddAsync(rentalRequest);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<RentalRequestResponseDto>(rentalRequest);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<AdminRentalRequestResponseDto>> GetPendingRequestsAsync()
        {
            var requests = await _rentalRequestRepository.GetPendingRequestsAsync();
            return _mapper.Map<IEnumerable<AdminRentalRequestResponseDto>>(requests);
        }

        public async Task<RentalRequestResponseDto> ApproveRequestAsync(Guid requestId, string? adminUserId)
        {
            if (string.IsNullOrEmpty(adminUserId))
            {
                throw new UnauthorizedException("Admin is not authenticated.");
            }

            var request = await _rentalRequestRepository.GetByIdAsync(requestId);
            if (request == null)
            {
                throw new NotFoundException("Rental request not found.");
            }

            if (request.Status != RentalRequestStatus.Pending)
            {
                throw new BadRequestException($"Cannot approve request that is already {request.Status}.");
            }

            request.Status = RentalRequestStatus.Approved;
            request.ReviewedByUserId = adminUserId;

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<RentalRequestResponseDto>(request);
        }

        public async Task<RentalRequestResponseDto> RejectRequestAsync(Guid requestId, string? adminUserId)
        {
            if (string.IsNullOrEmpty(adminUserId))
            {
                throw new UnauthorizedException("Admin is not authenticated.");
            }

            var request = await _rentalRequestRepository.GetByIdAsync(requestId);
            if (request == null)
            {
                throw new NotFoundException("Rental request not found.");
            }

            if (request.Status != RentalRequestStatus.Pending)
            {
                throw new BadRequestException($"Cannot reject request that is already {request.Status}.");
            }

            var book = request.Book;
            if (book == null)
            {
                throw new NotFoundException("Associated book not found.");
            }

            book.AvailableCopies += 1;
            request.Status = RentalRequestStatus.Rejected;
            request.ReviewedByUserId = adminUserId;

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<RentalRequestResponseDto>(request);
        }

        public async Task<IEnumerable<RentalRequestResponseDto>> GetUserRequestsAsync(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException("User is not authenticated.");
            }

            var requests = await _rentalRequestRepository.GetUserRequestsAsync(userId);
            return _mapper.Map<IEnumerable<RentalRequestResponseDto>>(requests);
        }
    }
}
