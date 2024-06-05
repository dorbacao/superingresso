import toastr from 'toastr';

export class authService {

    constructor() {

    }



    async login(login, senha) {

        const url = `${import.meta.env.VITE_APP_URL}/user/login`;

        var response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify({ 'login': login, 'senha': senha }),
            headers: {
                //'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json'
            }
        });

        console.log(response);
        if(!response.ok){
            toastr.error("Login e/ou senha inválidos");    
            console.log(response.statusText);
            return;
        }

        var token = await response.json();

        localStorage.setItem("login", JSON.stringify(token));

        toastr.success("Usuário autenticado com sucesso");

        setTimeout(()=>{
            window.location = "index.html";
        }, 1500);

        return token;
    }

}