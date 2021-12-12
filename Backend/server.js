//express 모듈 불러오기
const express = require("express");
const bodyParser = require('body-parser');
const mysql = require('mysql');
const dbconfig = require('./config/database.js');

const connection = mysql.createConnection(dbconfig);

//express 사용
const app = express();
app.use(bodyParser.json());

// DB 연결
connection.connect(err => {
    if (err) console.log("MySQL connect fail : ", err);
    console.log("MySQL Connected!");
});

//Login API
app.post("/user/login", async (req,res) => {
    // req.body를 통해 req 값 받아오기
    // console.log(req.body);

    const data = req.body;
    const queryString = `
        SELECT u_id, email, role, name 
        FROM Account 
        WHERE email='${data.email}' AND password='${data.password}'
    `;

    connection.query(queryString, (err, dbres) => {
        if (err) {console.log("query fail : ", err);}
        else {
            if (dbres.length == 0) {
                console.log('no match');
                let response = {
                    status: 404
                };
                res.send(response);
            }
            else {
                // response는 json 객체를 만들어 res.send를 통해 전달 
                let response = {
                    status: 200,
                    user: dbres[0]
                };
                res.send(response);
            }
        }
    });
});

//Logout API
app.post("/user/logout", (req,res) => {
    const data = req.body;
    const queryString = `
        SELECT *
        FROM Account 
        WHERE email='${data.email}' AND password='${data.password}'
    `;

    connection.query(queryString, (err, dbres) => {
        if (err) { console.log("query fail : ", err); }
        else {
            if (dbres.length == 0) {
                console.log('no match');
                let response = {
                    status: 404
                };
                res.send(response);
            }
            else {
                let response = {
                    status: 200
                };
                res.send(response);
            }
        }
    });
});

//Enter Detail Information API
app.put("/user/detail", (req,res) => {
    const data = req.body;
    const queryString = `
        UPDATE Account 
        SET role='${data.role}', name='${data.name}' 
        WHERE u_id='${data.u_id}'
    `;

    connection.query(queryString, (err, dbres) => {
        if (err) { console.log("query fail : ", err); }
        else {
            if (dbres.affectedRows == 0) {
                console.log('no match');
                let response = {
                    status: 404
                };
                res.send(response);
            }
            else {
                let response = {
                    status: 200
                };
                res.send(response);
            }
        }
    });
});

//Sighup API
app.post("/user", (req, res) => {
    const data = req.body;
    const queryString = `
        INSERT INTO Account (u_id, email, password)
        SELECT
            '${data.u_id}',
            '${data.email}',
            '${data.password}'
        FROM DUAL
        WHERE NOT EXISTS (
            SELECT *
            FROM Account
            WHERE u_id='${data.u_id}'
        )
    `;
    
    connection.query(queryString, (err, dbres) => {
        if (err) { console.log("query fail : ", err); }
        else {
            if (dbres.affectedRows == 0) {
                console.log('duplicated');
                let response = {
                    status: 404
                };
                res.send(response);
            }
            else {
                let response = {
                    status: 200
                };
                res.send(response);
            }
        }
    });
});

//Get Work Information API
app.post("/work/get", (req, res) => {
    const data = req.body;
    const queryString = `
        SELECT *
        FROM Work
        WHERE u_id='${data.u_id}'
    `;

    connection.query(queryString, (err, dbres) => {
        if (err) { console.log("query fail : ", err); }
        else {
            if (dbres.length == 0) {
                console.log('no match');
                let response = {
                    status: 404
                };
                res.send(response);
            }
            else {
                let work = dbres[0];
                let response = {
                    status: 200,
                    w_id: work.w_id,
                    u_id: work.u_id,
                    position_x: work.position_x,
                    position_y: work.position_y,
                    file_address: work.file_addr,
                    title: work.title,
                    description: work.description,
                    like_score: work.like_score
                };
                res.send(response);
            }
        }
    });
});

//Enter Work Information API
app.post("/work", (req, res)=>{
    const data = req.body;
    const queryString = `
        INSERT INTO Work (u_id, position_x, position_y, file_addr, title, description)
        SELECT 
            '${data.u_id}',
            ${data.position_x},
            ${data.position_y},
            '${data.file_addr}',
            '${data.title}',
            '${data.description}'
        FROM DUAL
        WHERE NOT EXISTS (
            SELECT *
            FROM Work
            WHERE u_id='${data.u_id}'
        )
    `;

    connection.query(queryString, (err, dbres) => {
        if (err) { console.log("query fail : ", err); }
        else {
            if (dbres.affectedRows == 0) {
                console.log('duplicated');
                let response = {
                    status: 404
                };
                res.send(response);
            }
            else {
                let response = {
                    status: 200
                };
                res.send(response);
            }
        }
    });
});

//Update Like API
app.put("/like", (req, res)=> {
    const data = req.body;
    const queryString = `
        UPDATE Work
        SET like_score=like_score+1
        WHERE u_id='${data.u_id}'
    `;

    connection.query(queryString, (err, dbres) => {
        if (err) { console.log("query fail : ", err); }
        else {
            if (dbres.affectedRows == 0) {
                console.log('no match');
                let response = {
                    status: 404
                };
                res.send(response);
            }
            else {
                const queryString2 = `
                    SELECT u_id, like_score
                    FROM Work 
                    WHERE u_id='${data.u_id}'
                `;

                connection.query(queryString2, (err, dbres) => {
                    if (err) { console.log("query fail : ", err); }
                    else if (dbres.length != 0) {
                        let work = dbres[0];
                        let response = {
                            status: 200,
                            u_id: work.u_id,
                            like_score: work.like_score
                        };
                        res.send(response);
                    }
                });
            }
        }
    });
});

//Get Like Scores API
app.get("/like/list", (req, res) => {
    const queryString = `
        SELECT W.w_id, W.u_id, A.name, W.title, W.like_score
        FROM Work W
        JOIN Account A ON W.u_id = A.u_id
    `;

    connection.query(queryString, (err, dbres) => {
        if (err) { console.log("query fail : ", err); }
        else {
            let response = {
                status: 200,
                list: dbres
            };
            res.send(response);
        }
    });
});

app.get("/", (req, res) => {
    res.send("Hello World");
});

// http listen port 생성 서버 실행
app.listen(80, () => console.log("start server!"));