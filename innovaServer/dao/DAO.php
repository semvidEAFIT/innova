<?php

include ('../dao/MysqlDBC.php');

class DAO {

    private $dbEngine;

    function __construct() {
        $this->dbEngine = MysqlDBC::getInstance();
    }

    function getUser($user, $password) {
        $user = $this->dbEngine->cleanVar($user);
        $password = $this->dbEngine->cleanVar($password);
        $result = $this->dbEngine->getResult(
                "SELECT * FROM user
                WHERE user = '$user'
                    AND password = '$password'");
        if ($this->dbEngine->getRowCount($result) == 0){
            return getErrorArray(
                    '01',
                    "No existe el usuario $user con la clave indicada");
        }else{
            return User::getUserObj($result->fetch_object())->toArray();
        }
    }

    // <editor-fold defaultstate="collapsed" desc="News Methods">
    function getNews() {
        $result = $this->dbEngine->getResult(
                "SELECT * FROM news");
        $NewsArray = array();
        while ($row = $result->fetch_object()) {
            $News = News::getNewsObject($row);
            array_push($NewsArray, $News);
        }
        return $NewsArray;
    }

    function getNewsById($id) {
        $id = $this->dbEngine->cleanVar($id);

        $result = $this->dbEngine->getResult(
                "SELECT * FROM news WHERE id = '$id' LIMIT 1");
        $row = $result->fetch_object();
        if ($row != null) {
            return News::getNewsObject($row);
        }
        return getErrorArray('01', "No existe una noticia con el id '$id'");
    }

    function createNews($News) {
        $id = $this->dbEngine->cleanVar($News->getId());
        $title = $this->dbEngine->cleanVar($News->getTitle());
        $brief = $this->dbEngine->cleanVar($News->getBrief());
        $content = $this->dbEngine->cleanVar($News->getContent());
        return $this->dbEngine->insert(
                        "INSERT INTO `news` (`id` ,`title` ,`brief` ,`content`)
                VALUES ('$id' ,'$title' ,'$brief' ,'$content' )"
        );
    }

    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="Comment Methods">
    function getComments($id_news) {
        $id_news = $this->dbEngine->cleanVar($id_news);

        $result = $this->dbEngine->getResult(
                "SELECT user.id, user.username, '' as password
                    FROM comment, user
                    WHERE comment.id_user = user.id
                        AND id_news = '$id_news'");
        $userArray = array();
        while ($row = $result->fetch_object()) {
            $User = User::getUserObject($row);
            $userArray[$User->getId()] = $User;
        }

        $result2 = $this->dbEngine->getResult(
                "SELECT * FROM comment
                    WHERE  id_news = '$id_news'");
        $commentArray = array();
        while ($row = $result2->fetch_object()) {
            $row->User = $userArray[$row->id_user];
            $Comment = Comment::getCommentObject($row);
            array_push($commentArray, $Comment);
        }
        return $commentArray;
    }

    function createComment($Comment) {
        $id = $this->dbEngine->getVar($Comment->getId());
        $id_news = $this->dbEngine->getVar($Comment->getId_news());
        $date = $this->dbEngine->getVar($Comment->getDate());
        $comment = $this->dbEngine->getVar($Comment->getComment());
        return $this->dbEngine->insert(
                        "INSERT INTO `comment` (`id` ,`id_news` ,`id_user` ,`date` ,`comment` )
	VALUES ('$id' ,'$id_news' ,'1' ,'$date' ,'$comment' )"
        );
    }
    // </editor-fold>
}

?>
