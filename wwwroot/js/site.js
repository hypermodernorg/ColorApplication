function SavePallet() {
    var activePallet = document.getElementById('activePallet').getElementsByTagName('button'); // The pallet container
    var palletName = document.getElementById('palletName').value;
    var palletDescription = document.getElementById('palletDescription').value;

    
    if (document.getElementById('palletPublic').checked == true) {
        var palletPublic = 1;
    }
    else {
        var palletPublic = 0;
    }

    if (document.getElementById('palletCopy').checked == true) {
        var palletCopy = 1;
    }
    else {
        var palletCopy = 0;
    }
    
    var colors = [];
    //alert(typeof palletCopy + "  " + palletCopy);
    //alert(typeof palletPublic+ "  " + palletPublic);
    for (var ele of activePallet) {
        colors.push(ele.style.backgroundColor);
    }

    var palletID = "";

    // IF there is a pallet id, set palletID to the id (GUID).
    if (document.getElementById('palletId')) {
        var palletID = document.getElementById('palletId').value;
        //alert("first" + palletID);
    }

    
    $.ajax({
        type: "POST",
        url: '/ColorPallets/SavePallets',
        data: {
            PalletID: palletID,
            Colors: colors,
            PalletName: palletName,
            PalletDescription: palletDescription,
            PalletIsPublic: palletPublic,
            PalletIsCopy: palletCopy
        },
        dataType: "json",
        success: function (PalletID) {
            var x = document.getElementById('offcanvasScrolling');
            if (!document.getElementById('palletId')) {
                var palletId = document.createElement('input');
                palletId.id = "palletId";
                palletId.value = PalletID;
                palletId.type = "hidden";
                x.appendChild(palletId);
                //alert("second" + PalletID);
            }
           
           
        },
        error: function (e) {
            alert('fail');
        }

    });
}

function SetFocusedColor(colorToAdd) {
    document.getElementById('colorInputId').value = colorToAdd;
    convertColor('colorInputId');

}

function DeleteColor(divToDelete) {
    var toDelete = document.getElementById(divToDelete);
    toDelete.remove();

}

function AddToPallet() {

   
    var colorToAdd = document.getElementById('colorInputId').value; // Color to add.
    var activePallet = document.getElementById('activePallet'); // Color will be added to this pallet.
  
    var randomID = "c" + Math.floor(Math.random() * 10000);

    activePallet.style.display = "flex";
    activePallet.style.flexWrap = "wrap";


    // ----- Color Buttons ----- //
    var dropdown = document.createElement("div");
    dropdown.classList.add('dropdown');
    dropdown.id = randomID + "d";

    // Button
    var dbutton = document.createElement("button");
    dbutton.style.width = "74px";
    dbutton.style.height = "74px";
    dbutton.style.backgroundColor = colorToAdd;
    dbutton.style.borderStyle = "none";
    dbutton.style.margin = "5px";
    dbutton.classList.add('btn', 'dropdown-toggle')
    dbutton.id = randomID;
    dbutton.type = "button";
    dbutton.setAttribute("data-bs-toggle", "dropdown");
    dbutton.setAttribute("aria-expanded", "false");
    dbutton.setAttribute("onclick", `SetFocusedColor('${colorToAdd}')`);
    // End Button

    // ul
    // <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
    var dul = document.createElement('ul');
    dul.classList.add('dropdown-menu');
    dul.setAttribute("aria-labelledby", dbutton.id);
    // end ul



    var li1 = document.createElement('li');
    var li2 = document.createElement('li');
    var li3 = document.createElement('li');

    var a1 = document.createElement('a');
    a1.classList.add("dropdown-item");
    a1.innerText = "Remove Color";
    a1.href = "#";
    a1.setAttribute("onclick", `DeleteColor('${randomID + 'd'}')`);
    var a2 = document.createElement('a');
    a2.classList.add("dropdown-item");
    a2.innerText = "test1";
    var a3 = document.createElement('a');
    a3.classList.add("dropdown-item");
    a3.innerText = "test1";

    li1.appendChild(a1);
    li2.appendChild(a2);
    li3.appendChild(a3);

    dul.appendChild(li1);
    dul.appendChild(li2);
    dul.appendChild(li3);

    dropdown.appendChild(dbutton);
    dropdown.appendChild(dul);
    activePallet.appendChild(dropdown);
    SavePallet();
}





