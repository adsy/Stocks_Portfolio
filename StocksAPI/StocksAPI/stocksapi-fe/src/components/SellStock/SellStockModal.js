import React, { useState } from "react";
import { Button } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import SellStockForm from "./SellStockForm";

function getModalStyle() {
  return {
    position: "fixed",
    top: "50%",
    left: "50%",
    /* bring your own prefixes */
    transform: "translate(-50%, -50%)",
  };
}

const useStyles = makeStyles((theme) => ({
  paper: {
    position: "absolute",
    width: 400,
    backgroundColor: "#171123",
    border: "5px solid #000000",
    color: "#F46036",
    boxShadow: theme.shadows[5],
    padding: theme.spacing(2, 4, 3),
    display: "flex",
    justifyContent: "center",
  },
}));

export default function SimpleModal({ stock, Update }) {
  const classes = useStyles();
  // getModalStyle is not a pure function, we roll the style only on the first render
  const [modalStyle] = useState(getModalStyle);
  const [open, setOpen] = useState(false);

  const handleOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const body = (
    <div style={modalStyle} className={classes.paper}>
      <SellStockForm handler={handleClose} stock={stock} Update={Update} />
    </div>
  );

  return (
    <div>
      <Button
        style={{ width: "95%" }}
        variant="contained"
        color="secondary"
        onClick={handleOpen}
      >
        X
      </Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="simple-modal-title"
        aria-describedby="simple-modal-description"
      >
        {body}
      </Modal>
    </div>
  );
}
