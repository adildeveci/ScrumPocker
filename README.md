<div id="top"></div>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/adildeveci/ScrumPocker">
    <img src="https://user-images.githubusercontent.com/21089760/156882552-0f8dba39-a62d-48c8-9206-0b8d58baefc1.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Scrum Pocker Api Project</h3>

  <p align="center">
    An awesome README file to jumpstart your projects!
    <br />
    <a href="https://github.com/adildeveci/ScrumPocker"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/adildeveci/ScrumPocker">View Demo</a>
    ·
    <a href="https://github.com/adildeveci/ScrumPocker/issues">Report Bug</a>
    ·
    <a href="https://github.com/adildeveci/ScrumPocker/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

![image](https://user-images.githubusercontent.com/21089760/156884874-9936e995-24db-48f7-a32a-35e6bb68213f.png)


This API project build for a real time scrum poker application.

* You can create a user
* You can use without registration
* You can list the rooms
* You can join the room
* You can create a room
* You can rate
* You can see the room detail

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

This section should list any major frameworks/libraries used to bootstrap your project. Leave any add-ons/plugins for the acknowledgements section. Here are a few examples.

* [.net core 6](https://docs.microsoft.com/tr-tr/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
* [Visual Studio](https://visualstudio.microsoft.com/tr/downloads/)  
* [Docker](https://www.docker.com/products/docker-desktop)
* npm
  ```sh
  npm install npm@latest -g
  ```  
* [Postman](https://www.postman.com/downloads/)

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/adildeveci/ScrumPocker.git
   ```
2. Build
   ```sh
   dotnet build
   ```
3. Run
   ```sh
   dotnet run
   ```   

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

You can use postman for api call. Or you can send a request directly SwaggerUI.
* Firs create a ClientToken
![image](https://user-images.githubusercontent.com/21089760/156886525-9faa0629-ff80-4750-8bf1-24bbc4bee300.png)

* Create an user token for room and other process with client token
![image](https://user-images.githubusercontent.com/21089760/156886744-30d6602a-c73d-4ce6-a3ff-810d67c836a6.png)
* Save user token with other request, and use it until expires
![image](https://user-images.githubusercontent.com/21089760/156886676-7b02ec6c-72d9-4ff8-aacd-23fb1ec3702f.png)
* If user token was expired then you can generate a new user token with refresh token
![image](https://user-images.githubusercontent.com/21089760/156886894-e653d50c-dcc1-47bc-8c28-08d01d2df370.png)

* You can create a room with an user token.
![image](https://user-images.githubusercontent.com/21089760/156886985-693cb3a9-e921-4363-a245-2478a7a8abf3.png)

* Some method requires a user token. Some method working with client token. But you can call all method with user token.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [x] Generate Api Project
    - [x] Use Swagger
    - [x] Use BaseReponse model
    - [x] Handle exception to BaseResponse
    - [x] Handle validation exception to BaseRepsonse
    - [x] Auth methods
    - [x] Role based auth 
- [ ] SignalR Hub methods
- [ ] Db entegrations
- [ ] CI/CD pipelines
- [ ] UI integration
    - [ ] Web
    - [ ] Mobile
    

See the [open issues](https://github.com/adildeveci/ScrumPocker/issues) for a full list of proposed features (and known).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Adil Deveci - [LinkedIn](https://www.linkedin.com/in/adildeveci/) - adildeveci@hotmail.com

Project Link: [https://github.com/adildeveci/ScrumPocker](https://github.com/adildeveci/ScrumPocker)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Udemy](https://www.udemy.com/)
* [Fatih Çakıroğlu](https://www.udemy.com/user/fatih-cakiroglu-2/)


<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/adildeveci/ScrumPocker?style=for-the-badge
[contributors-url]: https://github.com/adildeveci/ScrumPocker/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/adildeveci/ScrumPocker.svg?style=for-the-badge
[forks-url]: https://github.com/adildeveci/ScrumPocker/network/members
[stars-shield]: https://img.shields.io/github/stars/adildeveci/ScrumPocker.svg?style=for-the-badge
[stars-url]: https://github.com/adildeveci/ScrumPocker/stargazers
[issues-shield]: https://img.shields.io/github/issues/adildeveci/ScrumPocker?style=for-the-badge
[issues-url]: https://github.com/adildeveci/ScrumPocker/issues
[license-shield]: https://img.shields.io/github/license/adildeveci/ScrumPocker?style=for-the-badge
[license-url]: https://github.com/adildeveci/ScrumPocker/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/adildeveci 
