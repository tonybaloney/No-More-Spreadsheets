<%@ Page Title="Pricing Tool" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .menu_wrapper {
	        width: 100%; text-align: center; display: block;
        }
        .choice {
	        margin: 50px; width: 150px; height: 150px; color: white; display: inline-block; box-shadow: 5px 5px 5px #888; background-color: gray; -moz-box-shadow: 5px 5px 5px #888; -webkit-box-shadow: 5px 5px 5px #888;
        }
        .choice hover {
	        box-shadow: none;
        }
        .choice div {
	        height: 100px; text-align: center; font-size: 50pt;
        }
        .choice a {
	        height: 50px; text-align: center; font-weight: bold; text-decoration: none; display: block; background-color: darkgray; color: #111;
        }
        .choice a:hover {
            background-color: lightgray;
            color: #111;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="menu_wrapper">
        <div class="choice"> 
            <div class="icon_top">My</div>
            <a href="ViewQuotes.aspx">My Quotes</a>
        </div>
        <div class="choice"> 
            <div class="icon_top">Lo</div>
            <a href="Logout.aspx">Logout</a>
        </div>
        <br />
        <div class="choice"> 
            <div class="icon_top">Pr</div>
            <a href="Products.aspx">Products</a>
        </div>
        <div class="choice"> 
            <div class="icon_top">Re</div>
            <a href="ViewQuotes.aspx">Reports</a>
        </div>
    </div>
</asp:Content>

