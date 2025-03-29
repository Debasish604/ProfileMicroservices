using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileServices.Data;
using ProfileServices.Models.Dto;
using ProfileServices.Models.Entity;
using System;
using System.Linq;

namespace ProfileServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginServicesController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        public LoginServicesController(AppDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpPost("login/hiringManager")]
        public ActionResult<ResponseDto> LoginHiringManager([FromBody] LoginRequestdata request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest(new ResponseDto { IsSuccess = false, Message = "Email is required." });

            try
            {
                ApplicationUser? user = _dbContext.ApplicationUsers
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Email == request.Email);

                if (user == null)
                {
                    _response.Message = "Login failed. Email not found";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                if (user.IsHiringManager == "1")
                {
                    var result = _mapper.Map<ApplicationUserDto>(user);
                    _response.Message = "Login successful";
                    _response.Result = result;
                    return Ok(_response);
                }
                _response.Message = "Login failed. User is not a hiring manager.";
                _response.IsSuccess = false;
                return Unauthorized(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPost("login/candidate")]
        public ActionResult<ResponseDto> LoginCandidate([FromBody] LoginRequestdata request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                _response.Message = "Email is required";
                _response.IsSuccess = false;
                return BadRequest(_response);

            }

            try
            {
                ApplicationUser? user = _dbContext.ApplicationUsers
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Email == request.Email);

                if (user == null)
                {
                    _response.Message = "Login failed. Email not found";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                if (user.IsHiringManager == "0")
                {
                    var result = _mapper.Map<ApplicationUserDto>(user);
                    _response.Message = "Login successful";
                    _response.Result = result;
                    return Ok(_response);
                }
                _response.Message = "Login failed. User is not a hiring manager.";
                _response.IsSuccess = false;
                return Unauthorized(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPost("candidate/details")]
        public ActionResult<CandidateProfileDto> GetCandidateDetails([FromBody] LoginRequestdata request)
        {
           
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                {
                    _response.Message = "Email is required";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                CandidateProfile? candidate = _dbContext.CandidateProfiles
                    .AsNoTracking()
                    .FirstOrDefault(c => c.email == request.Email);

                if (candidate == null)
                {
                    _response.Message = "Candidate not found.";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                _response.Message = "Candidate details retrieved successfully.";
                _response.Result = _mapper.Map<CandidateProfileDto>(candidate);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }
    }
}
