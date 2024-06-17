import toastr from "./../components/toast";
import answer from "../my-components/answer";

export class authService {

    constructor() {

    }

    async createAndLogin(user) {

        const url = `${import.meta.env.VITE_APP_URL}/auth/local/signin`;

        var response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify(user),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        var tokenInfo = await response.json();

        answer.fromResponse(tokenInfo);

        localStorage.setItem("token", JSON.stringify(tokenInfo.value));

        setTimeout(() => {
            window.location = "index.html";
        }, 500);

        return token;
    }

    async loginFromLocal(login, senha) {

        const url = `${import.meta.env.VITE_APP_URL}/auth/local/token`;

        var response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify({ 'login': login, 'senha': senha }),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        var result = await response.json();

        answer.fromResponse(result);

        if(result.isOk){
            localStorage.setItem("token", JSON.stringify(result.value));

            setTimeout(() => {
                window.location = "index.html";
            }, 100);
        }
        return false;
    }

    async loginFromGoogle(credential) {
        try {

            var body = { idToken: credential };

            var url = `${import.meta.env.VITE_APP_URL}/auth/google/token`;

            var response = await fetch(url, {
                method: "POST",
                body: JSON.stringify(body),
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            var responseBody = await response.json();
            answer.fromResponse(responseBody);

            localStorage.setItem('token', JSON.stringify(responseBody.value));

        } catch (error) {
            answer.fromException(error);
            return false;
        }
        return true;
    }

    loginFromFacebook() {
        FB.login(function (response) {
            if (response.authResponse) {
                // O usuário foi autenticado com sucesso

                var url = `${import.meta.env.VITE_APP_URL}/auth/facebook/token`;

                var body = { accessToken: response.authResponse.accessToken };

                fetch(url, {
                    method: "POST",
                    body: JSON.stringify(body),
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .then(response => response.json())
                .then(jsonBody => { 
                    answer.fromResponse(jsonBody);
                    localStorage.setItem('token', jsonBody.value);
                    window.location = "index.html";
                });
            }
        }, { scope: 'public_profile' });

    }
}