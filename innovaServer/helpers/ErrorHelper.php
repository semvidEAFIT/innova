<?php

/**
 * @return the error message in an array format
 */
function getErrorArray($code, $message) {
    return array('Error' =>
        array('code' => $code,
            'message' => $message
        )
    );
}

?>
