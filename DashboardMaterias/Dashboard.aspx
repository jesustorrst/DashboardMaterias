﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DashboardMaterias.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-4">
                <div class="cardContainer inactive">
                    <div class="card">
                        <div class="side front">
                            <div class="img img1"></div>
                            <div class="info">
                                <h2>Super S</h2>
                                <p>A stand-on with an exceptional compact stance. Great for tight spaces and trailering.</p>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="cardContainer inactive">
                    <div class="card">
                        <div class="side front">
                            <div class="img img2"></div>
                            <div class="info">
                                <h2>Super Z HyperDrive</h2>
                                <p>A high-performance zero-turn with unsurpassed strength, speed &amp; reliability with a warranty to match.</p>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="cardContainer inactive">
                    <div class="card">
                        <div class="side front">
                            <div class="img img3"></div>
                            <div class="info">
                                <h2>Vanguard Power</h2>
                                <p>If you need a tough, commercial grade engine that makes you more productive, look to Vanguard.</p>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4">
            </div>
            <div class="col-md-4">
            </div>
        </div>

    </div>





    <style type="text/css">
        *, *:after, *:before {
            box-sizing: border-box;
        }

        h2, h4, p, ul, li {
            margin: 0;
            padding: 0;
        }

        h2, h4 {
            font-family: "Oswald", sans-serif;
            text-transform: uppercase;
            color: #333333;
        }

        h2 {
            font-size: 27px;
            font-weight: 500;
            letter-spacing: -0.2px;
            margin-bottom: 10px;
        }

        p, li {
            font-family: "Roboto", sans-serif;
            font-weight: 400;
            color: #555;
            line-height: 22px;
        }

        ul, li {
            text-decoration: none;
            list-style: disc outside;
        }

        ul {
            padding-left: 20px;
        }

        svg {
            margin: 0px;
            min-width: 24px;
            min-height: 24px;
        }

/*        body {
            background-color: #dadce2;
            background-image: linear-gradient(140deg, white, #dadce2);
            margin: 0;
            width: 100vw;
            min-height: 450px;
            height: 100vh;
            display: -webkit-box;
            display: flex;
            -webkit-box-align: center;
            align-items: center;
            -webkit-box-pack: center;
            justify-content: center;
        }*/

        .cardContainer {
            position: relative;
            width: 300px;
            height: 400px;
            min-width: 300px;
            min-height: 400px;
            margin: 4px;
            -webkit-perspective: 1000px;
            perspective: 1000px;
        }

        .active {
            -webkit-transform: translateZ(0px) rotateY(180deg) !important;
            transform: translateZ(0px) rotateY(180deg) !important;
        }

            .active:after {
                display: none;
            }

        .card {
            display: inline-block;
            width: 100%;
            height: 100%;
            cursor: pointer;
            -moz-backface-visibility: hidden;
            -webkit-transform-style: preserve-3d;
            transform-style: preserve-3d;
            -webkit-transform: translateZ(-100px);
            transform: translateZ(-100px);
            -webkit-transition: all 0.4s cubic-bezier(0.165, 0.84, 0.44, 1);
            transition: all 0.4s cubic-bezier(0.165, 0.84, 0.44, 1);
        }

            .card:after {
                content: "";
                position: absolute;
                z-index: -1;
                width: 100%;
                height: 100%;
                border-radius: 5px;
                box-shadow: 0 14px 50px -4px rgba(0, 0, 0, 0.15);
                opacity: 0;
                -webkit-transition: all 0.6s cubic-bezier(0.165, 0.84, 0.44, 1.4);
                transition: all 0.6s cubic-bezier(0.165, 0.84, 0.44, 1.4);
            }

            .card:hover {
                -webkit-transform: translateZ(0px);
                transform: translateZ(0px);
            }

                .card:hover:after {
                    opacity: 1;
                }

            .card .side {
                -webkit-backface-visibility: hidden;
                backface-visibility: hidden;
                position: absolute;
                width: 100%;
                height: 100%;
                border-radius: 5px;
                background-color: white;
            }

            .card .front {
                z-index: 2;
            }

            .card .back {
                -webkit-transform: rotateY(180deg);
                transform: rotateY(180deg);
            }

            .card .info {
                padding: 16px;
            }

        .front .img {
            background-color: #dadce2;
            background-position: center;
            background-size: cover;
            border-radius: 5px 5px 0 0;
            width: 100%;
            height: 250px;
        }

        .front .img1 {
            background-image: url(Iconos/IconosMaterias/Algebra.png);
        }

        .front .img2 {
            background-image: url(Iconos/IconosMaterias/Historia.png);
        }

        .front .img3 {
            background-image: url(Iconos/IconosMaterias/Quimica.png);
        }

        .back {
            position: relative;
        }

            .back h2 {
                margin-top: 6px;
                margin-bottom: 18px;
            }

            .back .reviews {
                display: -webkit-box;
                display: flex;
                -webkit-box-align: center;
                align-items: center;
                margin-bottom: 12px;
                cursor: pointer;
            }

                .back .reviews p {
                    color: #c4c4c4;
                    font-weight: 300;
                    margin: 1px 0 0 6px;
                    -webkit-transition: 0.3s ease-in-out;
                    transition: 0.3s ease-in-out;
                }

                .back .reviews:hover p {
                    color: #555;
                }

            .back li {
                line-height: 22px;
                margin: 2px 0 6px 0;
            }

            .back .btn {
                position: absolute;
                bottom: 16px;
                width: calc(100% - 32px);
                height: 56px;
                display: -webkit-box;
                display: flex;
                -webkit-box-align: center;
                align-items: center;
                -webkit-box-pack: center;
                justify-content: center;
                background-color: #FFC324;
                background-image: -webkit-gradient(linear, right top, left top, from(#FFB714), to(#FFE579));
                background-image: linear-gradient(-90deg, #FFB714, #FFE579);
                border-radius: 5px;
                cursor: pointer;
            }

                .back .btn:hover h4 {
                    -webkit-transform: translateX(0px);
                    transform: translateX(0px);
                }

                .back .btn:hover svg {
                    -webkit-transform: translateX(0px);
                    transform: translateX(0px);
                    opacity: 1;
                }

                .back .btn h4 {
                    -webkit-transform: translateX(12px);
                    transform: translateX(12px);
                    -webkit-transition: -webkit-transform 0.3s ease-out;
                    transition: -webkit-transform 0.3s ease-out;
                    transition: transform 0.3s ease-out;
                    transition: transform 0.3s ease-out, -webkit-transform 0.3s ease-out;
                }

                .back .btn svg {
                    margin: 1px 0 0 4px;
                    -webkit-transform: translateX(-8px);
                    transform: translateX(-8px);
                    opacity: 0;
                    -webkit-transition: all 0.3s ease-out;
                    transition: all 0.3s ease-out;
                }
    </style>

</asp:Content>
