

function searchReset(pSearchdropdownID, pSubmintbuttonID) {
    
        $(pSearchdropdownID).value = 0; //sets the SearchBox textbox to value of null (clears it)
       $(pSubmintbuttonID).click(); //Applies the filter of null by clicking the submit button automatically
   
    
    
}