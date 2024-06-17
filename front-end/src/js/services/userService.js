import answer from "./../my-components/answer";

function _toQueryString(obj) {
    if (!obj) return '';
    return '?' + Object.keys(obj)
        .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(obj[key])}`)
        .join('&');
}

function _paging(pagination = { pageSize: 10, pageIndex: 1 }) {
    return _toQueryString(pagination);
}

function _defaultHeader(otherHeaders) {
    var defaultHeader = {
        headers: {
            'Authorization': `Bearer ${_getToken()}`,
            'Content-Type': 'application/json'
        }
    };

    var concat = { ...defaultHeader, ...otherHeaders };

    return concat;
}
function _getToken() {
    return JSON.parse(localStorage.getItem('token')).token;
}

export class userService {

    constructor() {

    }

  
    
    async changeUserAsync(user) {

        const url = `${import.meta.env.VITE_APP_URL}/user/${user.id}`;

        var body = JSON.stringify(user);

        var response = await fetch(url, _defaultHeader({
            method: 'PUT',
            body: body
        }));

        var content = await response.json();
        answer.fromResponse(content);
        return content;
    }
   
  
    async getUserById(id) {
        try {

            const url = `${import.meta.env.VITE_APP_URL}/user/${id}`;

            var response = await fetch(url, _defaultHeader());

            var userDetail = await response.json();

            answer.fromResponse(userDetail);

            return userDetail.value;

        } catch (error) {
            answer.fromException(error);
        }
    }

    async changePassword(id, senha, confirmaSenha) {

        const url = `${import.meta.env.VITE_APP_URL}/user/${id}/password`;

        var response = await fetch(url, {
            method: 'PATCH',
            body: JSON.stringify({ 'senha': senha, 'confirmaSenha': confirmaSenha }),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        var userDetail = await response.json();

        return userDetail;
    }

    async getAllAsync(pagination) {

        const url = `${import.meta.env.VITE_APP_URL}/user${_paging(pagination)}`;

        var response = await fetch(url, _defaultHeader());

        var content = await response.json();

        answer.fromResponse(content);

        return content;
    }

}