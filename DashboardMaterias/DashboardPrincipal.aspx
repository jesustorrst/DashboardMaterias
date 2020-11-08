<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardPrincipal.aspx.cs" Inherits="DashboardMaterias.DashboardPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <div class="blog-slider">
                    <div class="blog-slider__wrp swiper-wrapper">
                        <div class="blog-slider__item swiper-slide">
                            <div class="blog-slider__img">
                                <img src="Iconos/IconosMaterias/videconference.png" alt="">
                            </div>
                            <div class="blog-slider__content">
                                <br />
                                <%--                    <span class="blog-slider__code">26 December 2019</span>--%>
                                <div class="blog-slider__title">AULA VIRTUAL</div>
                                <div class="blog-slider__text">En esta sección verás tu horario de clases y unirte a la sesión impartida por el profesor </div>
                                <a href="#" class="blog-slider__button">Continuar</a>
                            </div>
                        </div>




                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="blog-slider">
                    <div class="blog-slider__wrp swiper-wrapper">
                        <div class="blog-slider__item swiper-slide">
                            <div class="blog-slider__img">
                                <img src="https://res.cloudinary.com/muhammederdem/image/upload/v1535759872/kuldar-kalvik-799168-unsplash.jpg" alt="">
                            </div>
                            <div class="blog-slider__content">
                                <br />
                                <%--                    <span class="blog-slider__code">26 December 2019</span>--%>
                                <div class="blog-slider__title">MATERIAL DE APOYO Y TAREAS </div>
                                <div class="blog-slider__text">En esta sección podrás visualizar por materia el material de apoyo y las tareas que el profesor haya solicitado  </div>
                                <a href="#" class="blog-slider__button">Continuar</a>
                            </div>
                        </div>




                    </div>
                </div>
            </div>

        </div>

    </div>



    <style type="text/css">
        .container {
            padding-right: 5px;
            padding-left: 5px;
        }

        @import url("https://fonts.googleapis.com/css?family=Fira+Sans:400,500,600,700,800");

        * {
            box-sizing: border-box;
        }

        .blog-slider {
            width: 90%;
            position: relative;
            max-width: 800px;
            margin: auto;
            background: #fff;
            box-shadow: 0px 14px 80px rgba(34, 35, 58, 0.2);
            padding: 25px;
            border-radius: 25px;
            height: 300px;
            /* transition: all .3s;*/
        }

        @media screen and (max-width: 992px) {
            .blog-slider {
                max-width: 680px;
                height: 300px;
            }
        }

        @media screen and (max-width: 768px) {
            .blog-slider {
                min-height: 400px;
                height: auto;
                margin: 180px auto;
            }
        }

        @media screen and (max-height: 500px) and (min-width: 992px) {
            .blog-slider {
                height: 350px;
            }
        }

        .blog-slider__item {
            display: flex;
            align-items: center;
        }

        @media screen and (max-width: 768px) {
            .blog-slider__item {
                flex-direction: column;
            }
        }

        /*    .blog-slider__item.swiper-slide-active .blog-slider__img img {
            opacity: 1;
            transition-delay: .3s;
        }
*/

        .blog-slider__img {
            width: 200px;
            flex-shrink: 0;
            height: 200px;
            /*            background-image: linear-gradient(147deg, #fe8a39 0%, #fd3838 74%);*/
            box-shadow: 4px 13px 15px 1px #B0E0E6;
            border-radius: 20px;
            transform: translateX(-80px);
            overflow: hidden;
            background-repeat: no-repeat;
            background-position: center center;
            background-color: #ACE1AF;
            background-image: url(Iconos/IconosMaterias/tareas.png);
            background-size: contain;
            /*            background-position: right 10px top 10px;
*/
        }

            /*.blog-slider__img:after {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background-image:url(Iconos/IconosMaterias/tareas.png);
                background-size: contain;
                background-color:#ACE1AF;*/
            /*                background-image: linear-gradient(147deg, #E0FFFF 0%, #F0F8FF 74%);
*/ /*border-radius: 20px;
                opacity: 0.8;
            }*/

            .blog-slider__img img {
                width: 100%;
                height: 100%;
                object-fit: cover;
                display: block;
                opacity: 0;
                border-radius: 20px;
                transition: all .3s;
            }

        @media screen and (max-width: 768px) {
            .blog-slider__img {
                transform: translateY(-50%);
                width: 90%;
            }
        }

        @media screen and (max-width: 576px) {
            .blog-slider__img {
                width: 95%;
            }
        }

        @media screen and (max-height: 500px) and (min-width: 992px) {
            .blog-slider__img {
                height: 270px;
            }
        }

        .blog-slider__content {
            padding-right: 5px; /* 25*/
        }

        @media screen and (max-width: 768px) {
            .blog-slider__content {
                margin-top: -80px;
                text-align: center;
                padding: 0 30px;
            }
        }

        @media screen and (max-width: 576px) {
            .blog-slider__content {
                padding: 0;
            }
        }

        /* .blog-slider__content > * {
            opacity: 0;
            transform: translateY(25px);
            transition: all .4s;
        }*/

        .blog-slider__code {
            color: #7b7992;
            margin-bottom: 15px;
            display: block;
            font-weight: 500;
        }

        .blog-slider__title {
            font-size: 24px;
            font-weight: 700;
            color: #0d0925;
            margin-bottom: 20px;
        }

        .blog-slider__text {
            color: #4e4a67;
            margin-bottom: 30px;
            line-height: 1.5em;
        }

        .blog-slider__button {
            display: inline-flex;
            background-image: linear-gradient(147deg, #fe8a39 0%, #fd3838 74%);
            padding: 15px 35px;
            border-radius: 50px;
            color: #fff;
            box-shadow: 0px 14px 80px rgba(252, 56, 56, 0.4);
            text-decoration: none;
            font-weight: 500;
            justify-content: center;
            text-align: center;
            letter-spacing: 1px;
        }

        @media screen and (max-width: 576px) {
            .blog-slider__button {
                width: 100%;
            }
        }

        .blog-slider .swiper-container-horizontal > .swiper-pagination-bullets, .blog-slider .swiper-pagination-custom, .blog-slider .swiper-pagination-fraction {
            bottom: 10px;
            left: 0;
            width: 100%;
        }
    </style>

   
</asp:Content>
