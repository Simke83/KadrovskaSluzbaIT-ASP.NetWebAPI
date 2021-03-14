$(document).ready(function () {

    // podaci od interesa
    var host = window.location.host;

    var zaposleniEndpoint = "/api/Zaposleni/";
    var jediniceEndpoint = "/api/Jedinice/";
    var formAction = "Create";

    var token = null;
    var headers = {};


    loadZaposleni();

    $("#pocetnibutton1").click(function () {
        $("#regis").css("display", "none");
        $("#prij").css("display", "block");
    });

    $("#pocetnibutton2").click(function () {
        $("#regis").css("display", "block");
        $("#prij").css("display", "none");
    });

    $("#odustform").click(function () {
        refreshTable()
    });

    $("#odustajanjeprijava").click(function () {
        $("#korImee").val("");
        $("#loz").val("");
    });

    $("#odustajanjeregistracija").click(function () {
        $("#korIme").val("");
        $("#regLoz").val("");
        $("#regLoz2").val("");
    });

    $("#btnLista").click(function () {
        var reqUrlJedinice = "http://" + host + jediniceEndpoint;

        $.getJSON(reqUrlJedinice, setDropdown, "json");
    });

    $("#tradicija").click(function () {
        var reqUrl = 'http://' + host + "/api/tradicija";
        $.getJSON(reqUrl, Tradicija,"json");
    });

    $("#prikazZaposlenih").click(function () {
        loadZaposleni()
    });


    $("#btnLista").trigger("click");

    $("body").on("click", "#btnDelete", obrisiZaposlenog);

    //$("#btnDelete").click(function () {
    //    obrisiZaposlenog()
    //});


    function setDropdown(data, status) {
        if (status === "success") {

            console.log(data);
            var selectList = $("<select style='margin-bottom:20px' name='selectListRep' id='listaLanaca'></select>");

            for (let i = 0; i < data.length; i++) {
                var jedinica = data[i].Id.toString();
                var option = "<option value=" + jedinica + ">" + data[i].Ime + "</option>"
                selectList.append(option);
            }

            $(selectList).insertAfter("#jedinica");
        }
    }
   

    function loadZaposleni() {
        var requestUrl = 'http://' + host + zaposleniEndpoint;
        $.getJSON(requestUrl, setZaposleni);
    };


    function setZaposleni(data, status) {

        var $container = $("#OcitajZaposlene");
        $container.empty();

        if (status === "success") {

            var div = $("<div></div>");
            var h1 = $("<h1>Zaposleni</h1>");
            div.append(h1);

            var table = $("<table class='table table-bordered'></table>");
            var header;
            if (token) {
                header = $("<thead><tr><td>Ime i prezime</td><td>Rola</td><td>Godina zaposlenja</td><td>Godina rodjenja</td><td>Jedinica</td><td>Plata</td><td>Obrisi</td></tr></thead>");
            } else {
                header = $("<thead><tr><td>Ime i prezime</td><td>Rola</td><td>Godina zaposlenja</td><td>Godina rodjenja</td><td>Jedinica</td></thead></tr>");
            }

            table.append(header);
            tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {

                var row = "<tr>";
                var displayData = "<td>" + data[i].ImeIPrezime + "</td><td>" + data[i].Rola + "</td><td>" + data[i].GodinaZaposlenja + "</td><td>" + data[i].GodinaRodjenja + "</td><td>" + data[i].OrganizacionaJedinica.Ime + "</td>";
                var stringId = data[i].Id.toString();
                var displayDelete = "<td>" + data[i].Plata + "</td>" + "<td><button id=btnDelete name=" + stringId + ">Delete</button></td>";

                if (token) {
                    row += displayData + displayDelete + "</tr>";
                } else {
                    row += displayData + "</tr>";
                }
                tbody.append(row);
            }
            table.append(tbody);

            div.append(table);
            if (token) {
                $("#DodajZaposlenog").css("display", "block");
                $("#Pretraga").css("display", "block");
            }

            $container.append(div);
        }
        else {
            div = $("<div></div>");
            h1 = $("<h1>Greška prilikom preuzimanja Zaposlenih!</h1>");
            div.append(h1);
            $container.append(div);
        }
    };


    $("#registracija").submit(function (e) {
        e.preventDefault();

        var korIme = $("#korIme").val();
        var loz1 = $("#regLoz").val();
        var loz2 = $("#regLoz2").val();

        var sendData = {
            "Email": korIme,
            "Password": loz1,
            "ConfirmPassword": loz2

        };

        $.ajax({
            type: "POST",
            url: 'http://' + host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#info").append("Uspešna registracija. Možete se prijaviti na sistem.");
            $("#prij").css("display", "block");
            $("#regis").css("display", "none");
            $("#pocetni").css("display", "none");

        }).fail(function (data) {
            alert("Greska prilikom registracije!");
        });


    });


    $("#prijava").submit(function (e) {
        e.preventDefault();

        var korIme = $("#korImee").val();
        var loz = $("#loz").val();

        var sendData = {
            "grant_type": "password",
            "username": korIme,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#info").empty().append("Prijavljen korisnik: " + data.userName);
            token = data.access_token;
            $("#prij").css("display", "none");
            $("#regis").css("display", "none");
            $("#odjava").css("display", "block");
            $("#pocetni").css("display", "none");
            loadZaposleni();

        }).fail(function (data) {
            alert("Pogresan prilikom prijave!");
        });
    });


    $("#odjavise").click(function () {
        token = null;
        headers = {};

        $("#prij").css("display", "block");
        $("#pocetni").css("display", "block");
        $("#regis").css("display", "none");
        $("#odjava").css("display", "none");
        $("#info").empty();
        $("#sadrzaj").empty();
        $("#DodajZaposlenog").css("display", "none");
        $("#Pretraga").css("display", "none");

        loadZaposleni();

    });


    $("#dodajZaposlenog").submit(function (e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var orgJed = $("select[name ='selectListRep']").val();
        var Rola = $("#rola").val();
        var ImeiPrez = $("#imeiprezime").val();
        var GodRodj = $("#godinarodjenja").val();
        var GodZaposlenja = $("#godinazaposlenja").val();
        var Plata = $("#plata").val()
        var httpAction;
        var sendData;
        var url;

        if (formAction === "Create") {
            httpAction = "POST";
            url = 'http://' + host + zaposleniEndpoint;
            sendData = {
                "ImeIPrezime": ImeiPrez,
                "Rola": Rola,
                "GodinaRodjenja": GodRodj,
                "GodinaZaposlenja": GodZaposlenja,
                "Plata": Plata,
                "OrganizacionaJedinicaId": orgJed

            };
        }

        $.ajax({
            url: url,
            type: httpAction,
            headers: headers,
            data: sendData
        })
            .done(function (data, status) {
                formAction = "Create";
                refreshTable();
                loadZaposleni();
            })
            .fail(function (data, status) {
                alert("Greska prilikom dodavanja!!");
            })
    });


    function obrisiZaposlenog() {
        var deleteID = this.name;

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        $.ajax({
            url: 'http://' + host + zaposleniEndpoint + deleteID.toString(),
            type: "DELETE",
            headers: headers
        })
            .done(function (data, status) {
                loadZaposleni();
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            });
    };


    $("#pretragaPoPlati").submit(function (e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        var startGodina = $("#najmanje").val();
        var krajGodina = $("#najvise").val();

        var sendDataPretraga = {
            "Najmanje": startGodina,
            "Najvise": krajGodina
        };

        $.ajax({
            url: "http://" + host + "/api/pretraga",
            type: 'POST',
            data: sendDataPretraga,
            headers: headers,

        }).done(function (data, status) {
            console.log(data);
            $("#najmanje").val("");
            $("#najvise").val("");

            var $container = $("#OcitajZaposlene");
            $container.empty();

            if (token) {
                var div = $("<div></div>");
                var h1 = $("<h1>Zaposleni</h1>");
                div.append(h1);
                var table = $("<table class='table table-bordered'></table>");
                var header;
                if (token) {
                    header = $("<thead><tr><td>Ime i prezime</td><td>Rola</td><td>Godina zaposlenja</td><td>Godina rodjenja</td><td>Jedinica</td><td>Plata</td><td>Obrisi</td></tr></thead>");
                } else {
                    header = $("<thead><tr><td>Ime i prezime</td><td>Rola</td><td>Godina zaposlenja</td><td>Godina rodjenja</td><td>Jedinica</td></thead></tr>");
                }

                table.append(header);
                tbody = $("<tbody></tbody>");
                for (i = 0; i < data.length; i++) {
                    var row = "<tr>";
                    var displayData = "<td>" + data[i].ImeIPrezime + "</td><td>" + data[i].Rola + "</td><td>" + data[i].GodinaZaposlenja + "</td><td>" + data[i].GodinaRodjenja + "</td><td>" + data[i].OrganizacionaJedinica.Ime + "</td>";
                    var stringId = data[i].Id.toString();
                    var displayDelete = "<td>" + data[i].Plata + "</td>" + "<td><button id=btnDelete name=" + stringId + ">Delete</button></td>";

                    if (token) {
                        row += displayData + displayDelete + "</tr>";
                    } else {
                        row += displayData + "</tr>";
                    }
                    tbody.append(row);
                }
                table.append(tbody);
                div.append(table);
                $container.append(div);
            }
            else {
                div = $("<div></div>");
                h1 = $("<h1>Greška prilikom filtriranja Zaposlenih!</h1>");
                div.append(h1);
                $container.append(div);
            }
        });
    }); 


    function Tradicija(data,status) {
        var $container = $("#OcitajZaposlene");
        $container.empty();

        if (status === "success") {

            var div = $("<div></div>");
            var h1 = $("<h1>Zaposleni</h1>");
            div.append(h1);

            var table = $("<table class='table table-bordered'></table>");
            var header = $("<thead><tr><td>Ime</td><td>GodinaOsnivanja</td></tr>");
           

            table.append(header);
            tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {
                var displayData = "<tr><td>" + data[i].Ime + "</td><td>" + data[i].GodinaOsnivanja + "</td><tr>";
                tbody.append(displayData);
            }
            table.append(tbody);

            div.append(table);
            if (token) {
                $("#DodajZaposlenog").css("display", "block");
                $("#Pretraga").css("display", "block");
            }

            $container.append(div);
        }
        else {
            div = $("<div></div>");
            h1 = $("<h1>Greška prilikom preuzimanja Organizacionih jedinica!</h1>");
            div.append(h1);
            $container.append(div);
        }
    };
        

    function refreshTable() {
        $("#rola").val('');
        $("#imeiprezime").val('');
        $("#godinarodjenja").val('');
        $("#godinazaposlenja").val('');
        $("#plata").val('');
    };

});