<?php

/**
 * 
 */
class Controller {

    private $dao;
    // <editor-fold defaultstate="collapsed" desc="Singleton Section">
    private static $instance;

    public static function getInstance() {
        if (!self::$instance instanceof self) {
            self::$instance = new self;
        }
        return self::$instance;
    }

    // </editor-fold>

    function __construct() {
        $this->dao = new DAO();
    }

    // <editor-fold defaultstate="collapsed" desc="News Methods">
    public function getNews() {
        return $this->dao->getNews();
    }

    public function getNewsById($id) {
        $news = $this->dao->getNewsById($id);
        $comments = $this->dao->getComments($news->getId());
        $news->setComments($comments);
        return $news;
    }

    public function createNews($News) {
        return $this->dao->createNews($News);
    }

    // </editor-fold>
    // <editor-fold defaultstate="collapsed" desc="Comment Methods">
    function createComment($Comment) {
        return $this->dao->createComment($Comment);
    }

    // </editor-fold>
}

?>
