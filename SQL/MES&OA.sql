--Date:2019-09-25
--Author:caojin
--Description:For requirement "MES&SAP资料交换"

select  
cs.batch_id,
cs.lot_id,
lot.vendor_lot_id,
lot.qty,
cs.bank_id,
cs.order_no,
cs.lot_out_state 
from mmview.frlot lot
right join 
mmview.csfrout_state cs
on cs.lot_id=lot.lot_id