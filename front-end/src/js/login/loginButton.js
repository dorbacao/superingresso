
import toastr from 'toastr';
import 'toastr/build/toastr.min.css';
import { authService } from '../services/auth';

async function onSignIn(response){
    console.log('credential');
    console.log(response.credential);
    var body = {idToken:response.credential};
    
    var url = `${import.meta.env.VITE_APP_URL}/auth/google/token`;
    console.log(url);
  
    var response = await fetch(url, {
      method:"POST",
      body:JSON.stringify(body),
      headers: {
        'Content-Type': 'application/json'
      }
    });

    console.log(response);

    if(!response.ok){
        toastr.error(await response.text());
    }
  
    var token = await response.json();
    console.log(token);
    
    localStorage.setItem('token', JSON.stringify(token));

    //window.location = 'index.html';
    window.location = 'profile.html';
  }
  
function alteraLabelDoBotaoGoogle(){
    
    [...document.querySelectorAll('span')]      // get all the divs in an array
  .filter(txt => txt.innerHTML.includes('Sign in with Google')) // keep only those containing the query
  .forEach(txt => txt.innerHTML = 'Continue com o Google')

}
export function loginInit() {

    var clientId = import.meta.env.VITE_GOOGLE_CLIENT_ID;
    var loginGoogleButton = document.getElementById("login-google-button");
    google.accounts.id.initialize({
        'client_id': clientId,
        'callback': onSignIn
    });
    
    google.accounts.id.renderButton(loginGoogleButton, {
        shape:'square',
        type:'standard',
        theme: 'outline',
        size: 'large',
        locale: "pt",
        logo_alignment:'center',
        text: 'Continue com o Google',
        width:'370'
    });

    setTimeout(alteraLabelDoBotaoGoogle, 100);
    
    var loginInput = document.getElementById("loginInput");
    loginInput.focus();

    var senhaInput = document.getElementById("senhaInput");

    var keyPress = (e) => {
        if (e.key === 'Enter') {
            var btn = document.querySelector("#loginButton");
            btn.click();
        }
    };

    senhaInput.addEventListener("keypress", keyPress);
    loginInput.addEventListener("keypress", e => {
        var senha = document.getElementById("senhaInput");
        senha.focus();
    });

    document.getElementById("loginButton").addEventListener('click', async () => {
        var loginInput = document.getElementById("loginInput");
        var senhaInput = document.getElementById("senhaInput");

        var login = loginInput.value;
        var senha = senhaInput.value;

        if (!login) {
            loginInput.style.border = '1px solid #ef3c59';
            toastr.error("Login é obrigatório");
            loginInput.focus();
            return;
        } else {
            loginInput.style.border = '';
        }

        if (!senha) {
            senhaInput.style.border = '1px solid #ef3c59';;
            toastr.error("Senha é obrigatório");
            senhaInput.focus();
            return;
        } else {
            senhaInput.style.border = '';
        }

        var service = new authService();
        await service.login(login, senha);

    });
}