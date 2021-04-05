import React from "react";
import { Button } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import StockForm from "./StockForm";

function rand() {
  return Math.round(Math.random() * 20) - 10;
}

function getModalStyle() {
  const top = 50 + rand();
  const left = 50 + rand();

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

const SimpleModal = ({ CurrentStockPortfolio, PortfolioData, Update }) => {
  const classes = useStyles();
  // getModalStyle is not a pure function, we roll the style only on the first render
  const [modalStyle] = React.useState(getModalStyle);
  const [open, setOpen] = React.useState(false);
  const [buttonVisible, setButtonVisible] = React.useState(true);

  const handleOpen = () => {
    setOpen(true);
    setButtonVisible(false);
  };

  const handleClose = () => {
    setOpen(false);
    setButtonVisible(true);
  };

  const body = (
    <div style={modalStyle} className={classes.paper}>
      <StockForm
        handler={handleClose}
        CurrentStockPortfolio={CurrentStockPortfolio}
        PortfolioData={PortfolioData}
        Update={Update}
      />
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
        Buy The Dip 🚀
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
};

export default SimpleModal;
