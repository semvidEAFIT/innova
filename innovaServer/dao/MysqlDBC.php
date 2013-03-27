<?php

include ('../dao/Database.php');

/**
 * Description of MysqlDBC
 * Clase que provee la conexion directa a la base de datos mysql
 * utilizando las libreria de mysqli que provee php
 *
 * @author Rodrigo
 */
class MysqlDBC {

    // <editor-fold defaultstate="collapsed" desc="Singleton section">
    private static $mysqldbc;

    public static function getInstance() {
        if (MysqlDBC::$mysqldbc == null){
            MysqlDBC::$mysqldbc = new MysqlDBC();
        }
        return MysqlDBC::$mysqldbc;
    }
    // </editor-fold>

    private $connection; // mysql connection
    private $database;

    private function __construct() {
        $this->database = Database::getInstance();
        $this->connect();
    }

    // mysqli connection
    public function connect() {
        $this->connection = mysqli_connect(
                $this->database->getServer(),
                $this->database->getUser(),
                $this->database->getPassword(),
                $this->database->getDatabaseName())
                or $this->mysqlError();
        $this->connection->set_charset("utf8");
    }

    public function close(){
        mysqli_close($this->connection);
    }

    /**
     *
     * @param type $command
     * @return mysqli_result
     */
    public function getResult($command) {
        $result = mysqli_query($this->connection, ($command))
                or $this->mysqlErrorQuery($command);
        return $result;
    }

    public function cleanVar($var) {
        return $this->connection->real_escape_string($var);
    }

    public function insert($command) {
        $result = mysqli_query($this->connection, ($command))
                or $this->mysqlErrorQuery($command);
        return mysqli_insert_id($this->connection);
    }

    public function update($command) {
        $result = mysqli_query($this->connection, ($command))
                or $this->mysqlErrorQuery($command);
        return mysqli_affected_rows($this->connection);
    }

    public function delete($command) {
        $result = mysqli_query($this->connection, ($command))
                or $this->mysqlErrorQuery($command);
        return mysqli_affected_rows($this->connection);
    }

    public function mysqlError() {
        echo json_encode(getErrorArray(03,
                mysqli_connect_errno() . ' - ' . "Error de conexion:" . mysqli_connect_error()));
        die;
    }

    public function mysqlErrorQuery($command) {
        echo json_encode(getErrorArray(04,
                mysqli_errno($this->connection) . ' - ' . mysqli_error($this->connection) . ":'$command'"));
        mysqli_close($this->connection);
        die;
    }

    public function getRowCount($result) {
        return mysqli_num_rows($result);
    }
}

?>
