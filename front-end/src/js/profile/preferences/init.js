import toastr from 'toastr';
import { userService } from '../../services/userService';


export async function initPreferences(){
    document.getElementById("cancelarPreferenciasButton").addEventListener('click', ()=>{
        alert('cancelar preferencias');
        
    });

}