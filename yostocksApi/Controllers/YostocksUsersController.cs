using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using yostocksApi.Models;

namespace yostocksapi.Controllers
{
    [Authorize]
    public class YoStocksUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        // GET: api/YoStocksUsers
        public IQueryable<YostocksUser> GetYoStocksUsers()
        {
            return db.YostocksUsers;
        }


        //POST
        [HttpPost]
        [Route ("api/YostocksUsers/RegisterUser")]
        public async Task<IHttpActionResult> RegisterUser([FromBody] YostocksUser yostocksUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.YostocksUsers.Add(yostocksUser);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.Created);
        }

        [HttpPost]
        [Route ("api/YostocksUsers/GetUserProfile")]
        public YostocksUser GetUserProfile([FromBody] YoustocksUserProfileBindingModel userProfile)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            
            var result =  db.YostocksUsers.Where(p => p.Email.Equals(userProfile.Email)).First();

            YostocksUser user = new YostocksUser()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                City = result.City,
                Country = result.Country,
                Phone = result.Phone,
                ImagePath = result.ImagePath
            };
            
            return user;
            
        }


        [HttpPost]
        [Route("api/YostocksUsers/GetUserFragments")]
        public List<FragmentResponseModel> GetUserFragments([FromBody] UserFragmentModel userId)
        {
            
            ICollection<Fragment> fragments = db.Fragments.Where(p => p.YostocksUser.Id == userId.userId).ToArray();
            List<FragmentResponseModel> userFragments = new List<FragmentResponseModel>();
            foreach (Fragment f in fragments)
            {
                userFragments.Add(new FragmentResponseModel()
                {
                    Id = f.Id,
                    StockId = f.StockId,
                    PercentValue = f.PercentValue

                });
            }
            return userFragments;
        }

        



        //REGISTER NEW USER
        /*
        {"FirstName":"Grzegorz",
	"LastName":"Goraj",
	"Email":"greg.goray@gmail.com",
	"Phone":"+4560135579",
	"City":"Copenhagen",
	"Country":"Denmark",
	"ImagePath":"",
	"ImageArray":"/9j/4QCyRXhpZgAATU0AKgAAAAgABwESAAQAAAABAAAAAAEQAAIAAAAaAAAAYgEAAAQAAAABAAAB4IdpAAQAAAABAAAAmAEBAAQAAAABAAAB4AEyAAIAAAAUAAAAfAEPAAIAAAAIAAAAkAAAAABBbmRyb2lkIFNESyBidWlsdCBmb3IgeDg2ADIwMTg6MDM6MjAgMTA6NTU6NDgAdW5rbm93bgAAAZIIAAQAAAABAAAAAAAAAAD/4AAQSkZJRgABAQAAAQABAAD/2wBDAAIBAQEBAQIBAQECAgICAgQDAgICAgUEBAMEBgUGBgYFBgYGBwkIBgcJBwYGCAsICQoKCgoKBggLDAsKDAkKCgr/2wBDAQICAgICAgUDAwUKBwYHCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgr/wAARCAHgAeADASIAAhEBAxEB/8QAHAABAQEBAQEBAQEAAAAAAAAAAAkIBwYFAgQB/8QAPxABAAACBgkCBAMIAQMFAAAAAAQIAQYZlZbSAgMFGFZXWNPUaOQHFzd1EhOzERYiU5KUsdEyFBVSQVFhgrL/xAAdAQEAAQQDAQAAAAAAAAAAAAAABgEEBwgCAwkF/8QAPxEBAAADAgcLCQgDAAAAAAAAAAECAwUGEhY1NnJzsgQIFRgxU1WSk7PhE2NloqPB0dLiBxQiJVFSocIyQUL/2gAMAwEAAhEDEQA/APJgAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAADxW8vLn1AVIxVB5zeXlz6gKkYqg86QAD227LMb0/14wrGZDdlmN6f68YVjMivwCQO7LMb0/14wrGZDdlmN6f68YVjMivwCQO7LMb0/wBeMKxmQ3ZZjen+vGFYzIr8A8VvLy59QFSMVQec3l5c+oCpGKoPOkAAr/vLy59QFSMVQec3l5c+oCpGKoPOkAAr/vLy59QFSMVQec3l5c+oCpGKoPOkAA9tuyzG9P8AXjCsZkN2WY3p/rxhWMyK/AJA7ssxvT/XjCsZkN2WY3p/rxhWMyK/AJA7ssxvT/XjCsZkN2WY3p/rxhWMyK/APFby8ufUBUjFUHnN5eXPqAqRiqDzpAAK/wC8vLn1AVIxVB5zeXlz6gKkYqg86QACv+8vLn1AVIxVB5zeXlz6gKkYqg86QAD227LMb0/14wrGZDdlmN6f68YVjMivwCQO7LMb0/14wrGZDdlmN6f68YVjMivwCQO7LMb0/wBeMKxmQ3ZZjen+vGFYzIr8A8VvLy59QFSMVQec3l5c+oCpGKoPOkAAr/vLy59QFSMVQec3l5c+oCpGKoPOkAAr/vLy59QFSMVQec3l5c+oCpGKoPOkAA9tuyzG9P8AXjCsZkN2WY3p/rxhWMyK/AJA7ssxvT/XjCsZkN2WY3p/rxhWMyK/AJA7ssxvT/XjCsZkN2WY3p/rxhWMyK/APFby8ufUBUjFUHnN5eXPqAqRiqDzpAAK/wC8vLn1AVIxVB5zeXlz6gKkYqg86QACv+8vLn1AVIxVB5zeXlz6gKkYqg86QAD227LMb0/14wrGZDdlmN6f68YVjMivwCQO7LMb0/14wrGZDdlmN6f68YVjMivwCQO7LMb0/wBeMKxmQ3ZZjen+vGFYzIr8A8VvLy59QFSMVQec3l5c+oCpGKoPOkAAr/vLy59QFSMVQec3l5c+oCpGKoPOkAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAtoACJYC2giWAtoIlgAtoAiWLaAIli2gAIlgLaCJYC2giWAC2gCJYtoAiWLaAAiWAtoIlgLaCJYALaAIli2gCJYtoACJYC2giWAtoIlgAtoAiWLaAIli2gAIlgLaCJYC2giWAC2gCJYtoAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAkDvNTG9QFeMVRmc3mpjeoCvGKozODxIr/u0S59P9SMKweQ3aJc+n+pGFYPICQAr/ALtEufT/AFIwrB5Ddolz6f6kYVg8gJACv+7RLn0/1IwrB5Ddolz6f6kYVg8gPaiQO81Mb1AV4xVGZzeamN6gK8YqjM4K/CQO81Mb1AV4xVGZzeamN6gK8YqjM4K/CQO81Mb1AV4xVGZzeamN6gK8YqjM4PEiv+7RLn0/1IwrB5Ddolz6f6kYVg8gJACv+7RLn0/1IwrB5Ddolz6f6kYVg8gJACv+7RLn0/1IwrB5Ddolz6f6kYVg8gPaiQO81Mb1AV4xVGZzeamN6gK8YqjM4K/CQO81Mb1AV4xVGZzeamN6gK8YqjM4K/CQO81Mb1AV4xVGZzeamN6gK8YqjM4PEiv+7RLn0/1IwrB5Ddolz6f6kYVg8gJACv8Au0S59P8AUjCsHkN2iXPp/qRhWDyAkAK/7tEufT/UjCsHkN2iXPp/qRhWDyA9qJA7zUxvUBXjFUZnN5qY3qArxiqMzgr8JA7zUxvUBXjFUZnN5qY3qArxiqMzgr8JA7zUxvUBXjFUZnN5qY3qArxiqMzg8SK/7tEufT/UjCsHkN2iXPp/qRhWDyAkAK/7tEufT/UjCsHkN2iXPp/qRhWDyAkAK/7tEufT/UjCsHkN2iXPp/qRhWDyA9qJA7zUxvUBXjFUZnN5qY3qArxiqMzgr8JA7zUxvUBXjFUZnN5qY3qArxiqMzgr8JA7zUxvUBXjFUZnN5qY3qArxiqMzg8SK/7tEufT/UjCsHkN2iXPp/qRhWDyAkAK/wC7RLn0/wBSMKweQ3aJc+n+pGFYPICQAr/u0S59P9SMKweQ3aJc+n+pGFYPID2okDvNTG9QFeMVRmc3mpjeoCvGKozOCvwkDvNTG9QFeMVRmc3mpjeoCvGKozODxIALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAKj2c8m/J6jEG0u8Wc8m/J6jEG0u8DtolzaMzkc36Lg2b2S0ZnI5v0XBs3sgqMJc2jM5HN+i4Nm9ktGZyOb9FwbN7IKjCXNozORzfouDZvZLRmcjm/RcGzeyDiIqPZzyb8nqMQbS7xZzyb8nqMQbS7wJcCo9nPJvyeoxBtLvFnPJvyeoxBtLvAlwKj2c8m/J6jEG0u8Wc8m/J6jEG0u8DtolzaMzkc36Lg2b2S0ZnI5v0XBs3sgqMJc2jM5HN+i4Nm9ktGZyOb9FwbN7IKjCXNozORzfouDZvZLRmcjm/RcGzeyDiIqPZzyb8nqMQbS7xZzyb8nqMQbS7wJcCo9nPJvyeoxBtLvFnPJvyeoxBtLvAlwKj2c8m/J6jEG0u8Wc8m/J6jEG0u8DtolzaMzkc36Lg2b2S0ZnI5v0XBs3sgqMJc2jM5HN+i4Nm9ktGZyOb9FwbN7IKjCXNozORzfouDZvZLRmcjm/RcGzeyDiIqPZzyb8nqMQbS7xZzyb8nqMQbS7wJcCo9nPJvyeoxBtLvFnPJvyeoxBtLvAlwKj2c8m/J6jEG0u8Wc8m/J6jEG0u8DtolzaMzkc36Lg2b2S0ZnI5v0XBs3sgqMJc2jM5HN+i4Nm9ktGZyOb9FwbN7IKjCXNozORzfouDZvZLRmcjm/RcGzeyDiIqPZzyb8nqMQbS7xZzyb8nqMQbS7wJcCo9nPJvyeoxBtLvFnPJvyeoxBtLvAlwKj2c8m/J6jEG0u8Wc8m/J6jEG0u8DtolzaMzkc36Lg2b2S0ZnI5v0XBs3sgqMJc2jM5HN+i4Nm9ktGZyOb9FwbN7IKjCXNozORzfouDZvZLRmcjm/RcGzeyDiIqPZzyb8nqMQbS7xZzyb8nqMQbS7wJcCo9nPJvyeoxBtLvFnPJvyeoxBtLvA7aACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAACd1rhMbwZUi7YzyS1wmN4MqRdsZ5IMuCiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oNSCd1rhMbwZUi7YzyS1wmN4MqRdsZ5IKIid1rhMbwZUi7YzyS1wmN4MqRdsZ5IKIid1rhMbwZUi7YzyS1wmN4MqRdsZ5IMuCiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oNSCd1rhMbwZUi7YzyS1wmN4MqRdsZ5IKIid1rhMbwZUi7YzyS1wmN4MqRdsZ5IKIid1rhMbwZUi7YzyS1wmN4MqRdsZ5IMuCiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oNSCd1rhMbwZUi7YzyS1wmN4MqRdsZ5IKIid1rhMbwZUi7YzyS1wmN4MqRdsZ5IKIid1rhMbwZUi7YzyS1wmN4MqRdsZ5IMuCiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oNSCd1rhMbwZUi7YzyS1wmN4MqRdsZ5IKIid1rhMbwZUi7YzyS1wmN4MqRdsZ5IKIid1rhMbwZUi7YzyS1wmN4MqRdsZ5IMuCiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oJ3iiFkdLlxrXi8oPxSyOly41rxeUH4oNSCd1rhMbwZUi7YzyS1wmN4MqRdsZ5IKIid1rhMbwZUi7YzyS1wmN4MqRdsZ5IMuAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAA2zY3eomjCXuSxu9RNGEvcg22MSWyPp2oxb7YtkfTtRi32wNtjElsj6dqMW+2LZH07UYt9sDbYxJbI+najFvti2R9O1GLfbAxMNs2N3qJowl7ksbvUTRhL3IMTDbNjd6iaMJe5LG71E0YS9yDEw2zY3eomjCXuSxu9RNGEvcg22MSWyPp2oxb7YtkfTtRi32wNtjElsj6dqMW+2LZH07UYt9sDbYxJbI+najFvti2R9O1GLfbAxMNs2N3qJowl7ksbvUTRhL3IMTDbNjd6iaMJe5LG71E0YS9yDEw2zY3eomjCXuSxu9RNGEvcg22MSWyPp2oxb7YtkfTtRi32wNtjElsj6dqMW+2LZH07UYt9sDbYxJbI+najFvti2R9O1GLfbAxMNs2N3qJowl7ksbvUTRhL3IMTDbNjd6iaMJe5LG71E0YS9yDEw2zY3eomjCXuSxu9RNGEvcg22MSWyPp2oxb7YtkfTtRi32wNtjElsj6dqMW+2LZH07UYt9sDbYxJbI+najFvti2R9O1GLfbAxMNs2N3qJowl7ksbvUTRhL3IMTDbNjd6iaMJe5LG71E0YS9yDEw2zY3eomjCXuSxu9RNGEvcg22MSWyPp2oxb7YtkfTtRi32wNtjElsj6dqMW+2LZH07UYt9sDbYxJbI+najFvti2R9O1GLfbAxMNs2N3qJowl7ksbvUTRhL3IMTDbNjd6iaMJe5LG71E0YS9yDbYAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAMt2uMuXBVeLtg/KLXGXLgqvF2wflAneNR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAoiMt2uMuXBVeLtg/KLXGXLgqvF2wflA1IMt2uMuXBVeLtg/KLXGXLgqvF2wflA1IMt2uMuXBVeLtg/KLXGXLgqvF2wflAneNR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAoiMt2uMuXBVeLtg/KLXGXLgqvF2wflA1IMt2uMuXBVeLtg/KLXGXLgqvF2wflA1IMt2uMuXBVeLtg/KLXGXLgqvF2wflAneNR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAoiMt2uMuXBVeLtg/KLXGXLgqvF2wflA1IMt2uMuXBVeLtg/KLXGXLgqvF2wflA1IMt2uMuXBVeLtg/KLXGXLgqvF2wflAneNR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAoiMt2uMuXBVeLtg/KLXGXLgqvF2wflA1IMt2uMuXBVeLtg/KLXGXLgqvF2wflA1IMt2uMuXBVeLtg/KLXGXLgqvF2wflAneNR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAy4NR2R8xvGdSLyjPGLI+Y3jOpF5RnjAoiMt2uMuXBVeLtg/KLXGXLgqvF2wflA1IMt2uMuXBVeLtg/KLXGXLgqvF2wflAneAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAH/AK/sofqjVa2nR/HRq6aaP/eih+WnJV6sVc2p8KqI3a+wYKI11Efp0aOtiYbR1mlo6P7NH+H+J8m17UhZG5fKxlwvxMgfZvcWb7QLbqWbLW8nGFPCwsHC/wBwh+sP1UYGW/mDXzjja9467OfMGvnHG17x12dF8ePMet4M7cVv0n7H62pBlv5g18442veOuznzBr5xxte8ddnUx48x63gcVv0n7H62pBlv5g18442veOuznzBr5xxte8ddnMePMet4HFb9J+x+tPH8nW/ytL+k/J1v8rS/pbi/cipvCOzLv1eU/cipvCOzLv1eVyx4l5j+fB1cVyv0lDso/Mw7+Trf5Wl/Sfk63+Vpf0txfuRU3hHZl36vKfuRU3hHZl36vKY8S8x/PgcVyv0lDso/Mw7+Trf5Wl/Sfk63+Vpf0txfuRU3hHZl36vKfuRU3hHZl36vKY8S8x/PgcVyv0lDso/M2gMt/MGvnHG17x12c+YNfOONr3jrs6mPHmPW8HbxW/SfsfrakHgpctsbY25UmKjNsbUio3W6G1dPQ1esitfp6en+D8Gh/wCb3qb7k3R953NJV/dK1gvBZfAVtbps7CwvJVJpcL92DNggDvfIRLAAAAHbrOacjlBRf+ze8Wc05HKCi/8AZveBUYcStGJN+cNGH9pdktGJN+cNGH9pdkHbRxK0Yk35w0Yf2l2S0Yk35w0Yf2l2QdtHErRiTfnDRh/aXZLRiTfnDRh/aXZBLgdus5pyOUFF/wCze8Wc05HKCi/9m94HER26zmnI5QUX/s3vFnNORygov/ZveBxEdus5pyOUFF/7N7xZzTkcoKL/ANm94FRhxK0Yk35w0Yf2l2S0Yk35w0Yf2l2QdtHErRiTfnDRh/aXZLRiTfnDRh/aXZB20cStGJN+cNGH9pdktGJN+cNGH9pdkEuB26zmnI5QUX/s3vFnNORygov/AGb3gcRHbrOacjlBRf8As3vFnNORygov/ZveBxEdus5pyOUFF/7N7xZzTkcoKL/2b3gVGHErRiTfnDRh/aXZLRiTfnDRh/aXZB20cStGJN+cNGH9pdktGJN+cNGH9pdkHbRxK0Yk35w0Yf2l2S0Yk35w0Yf2l2QS4HbrOacjlBRf+ze8Wc05HKCi/wDZveBxEdus5pyOUFF/7N7xZzTkcoKL/wBm94HEWqZRPpJo/cNd/jReEs5pyOUFF/7N7zr/AMHPhD8RvgnUvRqj8T6u07M2prNfp6/Q1FMdqdb+LVaf8P4/x6nT09D/AJaOkiV8Y/lcul7othd7XCMb81dTNtSPVgMYQ5G84AAAAAAAAAAADussH0/jPvOn+jqXR2dPhzNh8A/gVsTW1T+Klff+17SiYr/q9RqP+1ROt/HqdPQ0ND8f49TqdPQ/56Gm+9aMSb84aMP7S7LNtk5MoaMuy8wL+572nr6neRdtHErRiTfnDRh/aXZLRiTfnDRh/aXZX6JJcDt1nNORygov/ZveLOacjlBRf+ze8DiI7dZzTkcoKL/2b3izmnI5QUX/ALN7wKjAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAAACJYAAAAALaAAAAODzPfUCF+zaH62ud4cHme+oEL9m0P1tch98smSaf9Ytit7RntX1E23I52AxlDkbxACoAAAAAAAAAAzHOh9UYD7Bqf1tc5G65Oh9UYD7Bqf1tc5GzbZOTKGjLsvMC/mfFp6+p3kQBfoktoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAAAAtoAAAA4PM99QIX7Nofra53hweZ76gQv2bQ/W1yH3yyZJp/1i2K3tGe1fUTbcjnYDGUORvEAKgAAAAAAAAADMc6H1RgPsGp/W1zkbrk6H1RgPsGp/W1zkbNtk5MoaMuy8wL+Z8Wnr6neRAF+iS2gAAAA8VvLy59QFSMVQec3l5c+oCpGKoPOCQA9tuyzG9P9eMKxmQ3ZZjen+vGFYzIDxI9tuyzG9P9eMKxmQ3ZZjen+vGFYzIDxI9tuyzG9P8AXjCsZkN2WY3p/rxhWMyAr8PFby8ufUBUjFUHnN5eXPqAqRiqDzg9qPFby8ufUBUjFUHnN5eXPqAqRiqDzg9qPFby8ufUBUjFUHnN5eXPqAqRiqDzgkAPbbssxvT/AF4wrGZDdlmN6f68YVjMgPEj227LMb0/14wrGZDdlmN6f68YVjMgPEj227LMb0/14wrGZDdlmN6f68YVjMgK/DxW8vLn1AVIxVB5zeXlz6gKkYqg84PajxW8vLn1AVIxVB5zeXlz6gKkYqg84PajxW8vLn1AVIxVB5zeXlz6gKkYqg84JAD227LMb0/14wrGZDdlmN6f68YVjMgPEj227LMb0/14wrGZDdlmN6f68YVjMgPEj227LMb0/wBeMKxmQ3ZZjen+vGFYzICvw8VvLy59QFSMVQec3l5c+oCpGKoPOD2o8VvLy59QFSMVQec3l5c+oCpGKoPOD2rg8z31Ahfs2h+trnQ95eXPqAqRiqDzuV/HGutTq+1shttVFrbszbcFq9maGrpi9l7R0IjU6Ol+Zp6X4aNPQ0/+X7dLR/rRC+WTJNL+sWxO9oz2r6ibbkeMAYxhyN4wBUAAAAAAAAAAZjnQ+qMB9g1P62ucjaAmi+D/AMWPiBX+F2xUP4X1j23BanY2hqtOL2VsPXRGp0NP87Xaf4Px6Gh/z/j0P63Nd2WY3p/rxhWMyM22Tkyhoy7LzAv5nxaevqd5F4ke23ZZjen+vGFYzIbssxvT/XjCsZkX6JK/DxW8vLn1AVIxVB5zeXlz6gKkYqg84PajxW8vLn1AVIxVB5zeXlz6gKkYqg84JAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAANUSifSSj7jrv8aLK7VEon0ko+467/GiiV8clQ0vdFsLva8+ampn2pHUAGL4cjecAVAAAAAAAAAAHdZYPp/GfedP9HUujucSwfT+M+86f6OpdHZtsnJlDRl2XmBfzPi09fU7yIAv0SRLAAAAABbQAAAAAESwAAAAAW0AAAAABEsAAAAAFtAAAAAARLAAAAaolE+klH3HXf40WV2qJRPpJR9x13+NFEr45Khpe6LYXe1581NTPtSOoAMXw5G84AqAAAAAAAAAAO6ywfT+M+86f6OpdHc4lg+n8Z950/wBHUujs22Tkyhoy7LzAv5nxaevqd5EAX6JIlgAAAAAtoAAAAACJYAAAAALaAAAAAAiWAAAAAC2gAAAAAIlgAAANUSifSSj7jrv8aLK7VEon0lo+467/APOiiV8clQ0vdFsJva8+ampm2pHUAGL4cjegAVAAAAAAAAAAHdZYPp/GfedP9HUujucSwfT+M+86f6OpdHZusrJlDRl2Xl/fzPi09fU24gC+RNEsAAAFtAARLAAAAABbQAAAAAESwAAAAAW0AAAAABEsAAAAAFtAAAAH8O1ar1X25E6MZtircBGa3Q0PwavTi4XQ09P8H/3f3Dqr7nobpkwKsmE+jZls2pYtb7xuCvNSm/xwpZppdlHf57fFzj3aP9zT/t/nz1+LnHu0v7mn/bygt+C7O5mXqwfXx3vl0jW68/xer+evxc492l/c0/7Pnr8XOPdpf3NP+3lA4Ls7mZerAx3vl0jW68/xer+evxc492l/c0/7Pnr8XOPdpf3NP+3lA4Ls7mZerAx3vl0jW68/xWc+X1Q+B9kXdqch8vqh8D7Iu7U5H2Bx4JszmJerK7Mfb8dJ1+0n+Z8f5fVD4H2Rd2pyHy+qHwPsi7tTkfYDgmzOYl6spj7fjpOv2k/zPj/L6ofA+yLu1OQ+X1Q+B9kXdqcj7AcE2ZzEvVlMfb8dJ1+0n+ZHb56/Fzj3aX9zT/s+evxc492l/c0/7eUHLguzuZl6sHVjvfLpGt15/i+lWWtNYa2bQo2lWLa0RGRFGh+HQ1sTr9LWU0aH/hR+J83/AOQoop0qf2UUftpXUktOnTwJEer1626601atNhTTfimmm/6AHN0raAAAA//Z"}*/





    }
}